import {
    Avatar,
    Box,
    Button,
    Card,
    CardActions,
    CardContent,
    CardMedia,
    Typography,
} from "@mui/material";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import { Link } from "react-router-dom";
import { Book } from "../../../app/models/book";
import { currencyFormat } from "../../../app/utils/utils";
import { LoadingButton } from "@mui/lab";
import { addBasketItemAsync } from "../../basket/basketSlice";

interface Props {
    book: Book;
}

export default function BookCard({ book }: Props) {
    const { status } = useAppSelector((state) => state.book);
    const dispatch = useAppDispatch();

    return (
        <Card>
            <Box sx={{ position: "relative" }}>
                <Avatar
                    sx={{
                        bgcolor: "secondary.main",
                        position: "absolute",
                        top: 5,
                        right: 5,
                        zIndex: 1,
                        width: 32,
                        height: 32,
                        fontSize: "0.75rem",
                    }}
                >
                    {`${book?.discount}%`}
                </Avatar>

                <CardMedia
                    sx={{
                        height: 180,
                        backgroundSize: "contain",
                        bgcolor: "primary.light",
                    }}
                    image={book?.imageUrl || "/images/default-book.jpg"}
                    title={book?.name}
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
                    {book?.name}
                </Typography>

                <Typography
                    gutterBottom
                    color="secondary"
                    variant="h5"
                    component="div"
                    sx={{ fontWeight: "bold", marginBottom: 1 }}
                >
                    {currencyFormat(book?.price * (1 - book.discount / 100))}
                </Typography>

                {book.discount > 0 && (
                    <Typography
                        gutterBottom
                        color="text.secondary"
                        variant="body1"
                        component="div"
                        sx={{
                            textDecoration: "line-through",
                        }}
                    >
                        {currencyFormat(book?.price)}
                    </Typography>
                )}
            </CardContent>

            <CardActions>
                <Box
                    sx={{
                        display: "flex",
                        justifyContent: "space-between",
                        width: "100%",
                    }}
                >
                    <Button
                        component={Link}
                        to={`/book/${book?.id}`}
                        size="small"
                    >
                        Xem Chi tiết
                    </Button>
                    <LoadingButton
                        loading={status === "pendingAddItem" + book?.id}
                        onClick={() =>
                            dispatch(addBasketItemAsync({ bookId: book?.id }))
                        }
                        size="small"
                    >
                        Thêm vào giỏ hàng
                    </LoadingButton>
                </Box>
            </CardActions>
        </Card>
    );
}
