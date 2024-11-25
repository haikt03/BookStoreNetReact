import {
    Grid,
    Card,
    Skeleton,
    CardContent,
    CardActions,
    Box,
} from "@mui/material";

export default function AuthorCardSkeleton() {
    return (
        <Grid item xs>
            <Card>
                <Box sx={{ position: "relative" }}>
                    <Skeleton
                        sx={{
                            height: 180,
                            backgroundSize: "contain",
                        }}
                        animation="wave"
                        variant="rectangular"
                    />
                </Box>
                <CardContent sx={{ pb: 0 }}>
                    <Skeleton
                        animation="wave"
                        height={20}
                        width="80%"
                        style={{ marginBottom: 8 }}
                    />
                    <Skeleton animation="wave" height={15} width="60%" />
                </CardContent>
                <CardActions>
                    <Skeleton animation="wave" height={30} width="40%" />
                </CardActions>
            </Card>
        </Grid>
    );
}
