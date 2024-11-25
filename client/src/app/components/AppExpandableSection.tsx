import { IconButton } from "@mui/material";
import { ExpandLess, ExpandMore } from "@mui/icons-material";

interface ExpandableSectionProps {
    showExpand?: boolean;
    isCollapsed: boolean;
    onExpand: () => void;
    onCollapse: () => void;
}

export default function AppExpandableSection({
    showExpand,
    isCollapsed,
    onExpand,
    onCollapse,
}: ExpandableSectionProps) {
    return (
        <>
            {showExpand && isCollapsed && (
                <IconButton
                    onClick={onCollapse}
                    sx={{ display: "flex", margin: "0 auto" }}
                >
                    <ExpandLess />
                </IconButton>
            )}
            {showExpand && !isCollapsed && (
                <IconButton
                    onClick={onExpand}
                    sx={{ display: "flex", margin: "0 auto" }}
                >
                    <ExpandMore />
                </IconButton>
            )}
        </>
    );
}
