import {
    Box,
    Button,
    Card,
    CardActions,
    CardContent,
    CardMedia,
    Typography,
} from "@mui/material";
import { Author } from "../../../app/models/author";
import { Link } from "react-router-dom";

interface Props {
    author: Author;
}

export default function AuthorCard({ author }: Props) {
    return (
        <Card>
            <Box sx={{ position: "relative" }}>
                <CardMedia
                    sx={{
                        height: 180,
                        backgroundSize: "contain",
                        bgcolor: "primary.light",
                    }}
                    image={author?.imageUrl || "/images/default-author.jpg"}
                    title={author?.fullName}
                />
            </Box>
            <CardContent sx={{ pb: 0 }}>
                <Typography
                    sx={{
                        fontWeight: "bold",
                        color: "primary.main",
                        display: "-webkit-box",
                        overflow: "hidden",
                        textOverflow: "ellipsis",
                        WebkitBoxOrient: "vertical",
                        WebkitLineClamp: 2,
                        marginBottom: 1,
                    }}
                >
                    {author?.fullName}
                </Typography>
                <Typography
                    gutterBottom
                    color="text.secondary"
                    variant="body1"
                    component="div"
                >
                    {author.country}
                </Typography>
            </CardContent>

            <CardActions>
                <Button
                    component={Link}
                    to={`/author/${author?.id}`}
                    size="small"
                >
                    Xem Chi tiết
                </Button>
            </CardActions>
        </Card>
    );
}
