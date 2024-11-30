import { FieldValues, useForm } from "react-hook-form";
import { BookDetail } from "../../../app/models/book";
import { yupResolver } from "@hookform/resolvers/yup";
import { bookValidationSchema } from "./bookValidation";
import useBooks from "../../../app/hooks/useBooks";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import { useEffect } from "react";
import agent from "../../../app/api/agent";
import { Box, Button, Grid, Paper, Typography } from "@mui/material";
import AppTextInput from "../../../app/components/AppTextInput";
import AppSelectList from "../../../app/components/AppSelectList";
import AppDropzone from "../../../app/components/AppDropzone";
import { LoadingButton } from "@mui/lab";
import { getBookAsync, setBook } from "../bookSlice";
import LoadingComponent from "../../../app/layout/LoadingComponent";

interface Props {
    bookId?: number;
    cancelEdit: () => void;
}

export default function BookForm({ bookId, cancelEdit }: Props) {
    const {
        control,
        reset,
        handleSubmit,
        watch,
        formState: { isDirty, isSubmitting },
    } = useForm({
        mode: "onTouched",
        resolver: yupResolver<any>(bookValidationSchema),
    });
    const { authorsForUpsert } = useBooks();
    const watchFile = watch("file", null);
    const dispatch = useAppDispatch();
    const { bookDetail, bookDetailLoaded } = useAppSelector(
        (state) => state.book
    );

    useEffect(() => {
        if (bookId) dispatch(getBookAsync(bookId));
    }, []);

    useEffect(() => {
        if (bookDetail && !watchFile && !isDirty)
            reset({
                ...bookDetail,
                authorId: bookDetail.author?.id || "",
            });
        return () => {
            if (watchFile) URL.revokeObjectURL(watchFile.preview);
        };
    }, [bookDetail, reset, watchFile, isDirty]);

    async function handleSubmitData(data: FieldValues) {
        let response: BookDetail;
        if (bookDetail) {
            response = await agent.book.updateBook(bookDetail.id, data);
        } else {
            response = await agent.book.createBook(data);
        }
        dispatch(setBook(response));
        cancelEdit();
    }

    if (bookId && !bookDetailLoaded) {
        return <LoadingComponent />;
    }

    return (
        <Box component={Paper} sx={{ p: 4 }}>
            <Typography
                variant="h4"
                gutterBottom
                sx={{ mb: 4 }}
                color="primary.light"
            >
                {bookDetail ? "Cập nhật thông tin sách" : "Thêm mới sách"}
            </Typography>
            <form onSubmit={handleSubmit(handleSubmitData)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="name"
                            label="Tên sách"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="category"
                            label="Thể loại"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppSelectList
                            items={authorsForUpsert.map((author) => ({
                                id: author.id,
                                name: author.fullName,
                            }))}
                            control={control}
                            name="authorId"
                            label="Tác giả"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="translator"
                            label="Người dịch"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="language"
                            label="Ngôn ngữ"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="form"
                            label="Hình thức"
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <AppTextInput
                            multiline={true}
                            rows={4}
                            control={control}
                            name="description"
                            label="Description"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="publisher"
                            label="Nhà xuất bản"
                        />
                    </Grid>

                    <Grid item xs={12} sm={4}>
                        <AppTextInput
                            type="number"
                            control={control}
                            name="publishedYear"
                            label="Năm xuất bản"
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput
                            type="number"
                            control={control}
                            name="weight"
                            label="Trọng lượng"
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput
                            type="number"
                            control={control}
                            name="numberOfPages"
                            label="Số trang"
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput
                            type="number"
                            control={control}
                            name="price"
                            label="Giá"
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput
                            type="number"
                            control={control}
                            name="discount"
                            label="Giảm giá"
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput
                            type="number"
                            control={control}
                            name="quantityInStock"
                            label="Số lượng trong kho"
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <Box
                            display="flex"
                            justifyContent="space-between"
                            alignItems="center"
                        >
                            <AppDropzone control={control} name="file" />
                            {watchFile ? (
                                <img
                                    src={watchFile.preview}
                                    alt="preview"
                                    style={{ maxHeight: 200 }}
                                />
                            ) : (
                                <img
                                    src={bookDetail?.imageUrl}
                                    alt={bookDetail?.name}
                                    style={{ maxHeight: 200 }}
                                />
                            )}
                        </Box>
                    </Grid>
                </Grid>
                <Box
                    display="flex"
                    justifyContent="space-between"
                    sx={{ mt: 3 }}
                >
                    <Button
                        onClick={cancelEdit}
                        variant="contained"
                        color="inherit"
                    >
                        Huỷ
                    </Button>
                    <LoadingButton
                        loading={isSubmitting}
                        type="submit"
                        variant="contained"
                        color="success"
                    >
                        {bookDetail ? "Cập nhật" : "Thêm mới"}
                    </LoadingButton>
                </Box>
            </form>
        </Box>
    );
}
