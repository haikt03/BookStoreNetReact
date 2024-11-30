import { Box, IconButton } from "@mui/material";
import { ExpandLess, ExpandMore } from "@mui/icons-material";

interface ExpandableSectionProps {
    showExpand: boolean;
    isCollapsed: boolean;
    onExpand: () => void;
    onCollapse: () => void;
    single?: boolean;
}

export default function AppExpandableSection({
    showExpand,
    isCollapsed,
    onExpand,
    onCollapse,
    single,
}: ExpandableSectionProps) {
    return (
        <Box>
            {isCollapsed && (
                <IconButton
                    onClick={onCollapse}
                    sx={{ display: "flex", margin: "0 auto" }}
                >
                    <ExpandLess />
                </IconButton>
            )}
            {single
                ? showExpand &&
                  !isCollapsed && (
                      <IconButton
                          onClick={onExpand}
                          sx={{ display: "flex", margin: "0 auto" }}
                      >
                          <ExpandMore />
                      </IconButton>
                  )
                : showExpand && (
                      <IconButton
                          onClick={onExpand}
                          sx={{ display: "flex", margin: "0 auto" }}
                      >
                          <ExpandMore />
                      </IconButton>
                  )}
        </Box>
    );
}
