import {
    Grid,
    Card,
    Skeleton,
    CardContent,
    CardActions,
    Box,
} from "@mui/material";

export default function BookCardSkeleton() {
    return (
        <Grid item xs component={Card}>
            <Box sx={{ position: "relative" }}>
                <Skeleton
                    variant="rectangular"
                    sx={{
                        position: "absolute",
                        top: 5,
                        right: 5,
                        zIndex: 1,
                        borderRadius: "50%",
                        width: 32,
                        height: 32,
                    }}
                />
                <Skeleton
                    sx={{ height: 180, backgroundSize: "contain" }}
                    animation="wave"
                    variant="rectangular"
                />
            </Box>
            <CardContent sx={{ pb: 0 }}>
                <Skeleton
                    animation="wave"
                    height={20}
                    width="90%"
                    style={{ marginBottom: 10 }}
                />
                <Skeleton
                    animation="wave"
                    height={30}
                    width="60%"
                    style={{ marginBottom: 10 }}
                />
                <Skeleton
                    animation="wave"
                    height={20}
                    width="40%"
                    style={{ textDecoration: "line-through" }}
                />
            </CardContent>
            <CardActions>
                <Box
                    sx={{
                        display: "flex",
                        justifyContent: "space-between",
                        width: "100%",
                    }}
                >
                    <Skeleton
                        animation="wave"
                        height={30}
                        width="38%"
                        variant="rectangular"
                        style={{ borderRadius: 4 }}
                    />
                    <Skeleton
                        animation="wave"
                        height={30}
                        width="58%"
                        variant="rectangular"
                        style={{ borderRadius: 4 }}
                    />
                </Box>
            </CardActions>
        </Grid>
    );
}
