import { Box, Checkbox, FormControlLabel, Typography } from "@mui/material";
import { buildCategoryTree } from "../../app/utils/utils";
import { Category } from "../../app/models/category";
import AppExpandableSection from "../../app/components/AppExpandableSection";
import { useState } from "react";

interface CategoryFilterProps {
    categories: Category[];
    checkedCategories: number[];
    onCategoryChange: (selectedCategories: number[]) => void;
}

export default function AppCategoryFilter({
    categories,
    checkedCategories,
    onCategoryChange,
}: CategoryFilterProps) {
    const tree = buildCategoryTree(categories);

    // Track which nodes are expanded
    const [expandedNodes, setExpandedNodes] = useState<number[]>([]);

    const toggleExpandNode = (id: number) => {
        if (expandedNodes.includes(id)) {
            setExpandedNodes(expandedNodes.filter((nodeId) => nodeId !== id));
        } else {
            setExpandedNodes([...expandedNodes, id]);
        }
    };

    const renderCategoryTree = (nodes: any[]) => {
        return nodes.map((node) => {
            const isLeaf = !node.children || node.children.length === 0;
            const isExpanded = expandedNodes.includes(node.id);

            return (
                <Box key={node.id} sx={{ pl: 2 }}>
                    <Box sx={{ display: "flex", alignItems: "center" }}>
                        {!isLeaf && (
                            <AppExpandableSection
                                isCollapsed={isExpanded}
                                onExpand={() => toggleExpandNode(node.id)}
                                onCollapse={() => toggleExpandNode(node.id)}
                                showExpand={true}
                            />
                        )}
                        <Typography variant="body2" sx={{ flexGrow: 1 }}>
                            {node.name}
                        </Typography>
                        {isLeaf && (
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={checkedCategories.includes(
                                            node.id
                                        )}
                                        onChange={(e) => {
                                            const newChecked = e.target.checked
                                                ? [
                                                      ...checkedCategories,
                                                      node.id,
                                                  ]
                                                : checkedCategories.filter(
                                                      (id) => id !== node.id
                                                  );
                                            onCategoryChange(newChecked);
                                        }}
                                    />
                                }
                                label=""
                            />
                        )}
                    </Box>
                    {!isLeaf && isExpanded && renderCategoryTree(node.children)}
                </Box>
            );
        });
    };

    return <Box>{renderCategoryTree(tree)}</Box>;
}
