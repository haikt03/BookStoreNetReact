import { Grid } from "@mui/material";
import { Book } from "../../../app/models/book";
import { useAppSelector } from "../../../app/store/configureStore";
import BookCardSkeleton from "./BookCardSkeleton";
import BookCard from "./BookCard";

interface Props {
    books: Book[];
}

export default function BookList({ books }: Props) {
    const { booksLoaded } = useAppSelector((state) => state.book);
    return (
        <Grid container spacing={4}>
            {books.map((book) => (
                <Grid item xs={4} key={book.id}>
                    {!booksLoaded ? (
                        <BookCardSkeleton />
                    ) : (
                        <BookCard book={book} />
                    )}
                </Grid>
            ))}
        </Grid>
    );
}
