import { Grid } from "@mui/material";
import { Author } from "../../../app/models/author";
import { useAppSelector } from "../../../app/store/configureStore";
import AuthorCardSkeleton from "./AuthorCardSkeleton";
import AuthorCard from "./AuthorCard";

interface Props {
    authors: Author[];
}

export default function AuthorList({ authors }: Props) {
    const { authorsLoaded } = useAppSelector((state) => state.author);
    return (
        <Grid container spacing={4}>
            {authors.map((author) => (
                <Grid item xs={4} key={author.id}>
                    {!authorsLoaded ? (
                        <AuthorCardSkeleton />
                    ) : (
                        <AuthorCard author={author} />
                    )}
                </Grid>
            ))}
        </Grid>
    );
}
