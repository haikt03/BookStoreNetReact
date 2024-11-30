import { yupResolver } from "@hookform/resolvers/yup";
import { FieldValues, useForm } from "react-hook-form";
import { authorValidationSchema } from "./authorValidation";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import { useEffect } from "react";
import { getAuthorAsync, setAuthor } from "../authorSlice";
import { AuthorDetail } from "../../../app/models/author";
import agent from "../../../app/api/agent";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Box, Button, Grid, Paper, Typography } from "@mui/material";
import AppTextInput from "../../../app/components/AppTextInput";
import AppDropzone from "../../../app/components/AppDropzone";
import { LoadingButton } from "@mui/lab";

interface Props {
    authorId?: number;
    cancelEdit: () => void;
}

export default function AuthorForm({ authorId, cancelEdit }: Props) {
    const {
        control,
        reset,
        handleSubmit,
        watch,
        formState: { isDirty, isSubmitting },
    } = useForm({
        mode: "onTouched",
        resolver: yupResolver<any>(authorValidationSchema),
    });
    const watchFile = watch("file", null);
    const dispatch = useAppDispatch();
    const { authorDetail, authorDetailLoaded } = useAppSelector(
        (state) => state.author
    );

    useEffect(() => {
        if (authorId) dispatch(getAuthorAsync(authorId));
    }, []);

    useEffect(() => {
        if (authorDetail && !watchFile && !isDirty) reset(authorDetail);
        return () => {
            if (watchFile) URL.revokeObjectURL(watchFile.preview);
        };
    }, [authorDetail, reset, watchFile, isDirty]);

    async function handleSubmitData(data: FieldValues) {
        let response: AuthorDetail;
        if (authorDetail) {
            response = await agent.author.updateAuthor(authorDetail.id, data);
        } else {
            response = await agent.author.createAuthor(data);
        }
        dispatch(setAuthor(response));
        cancelEdit();
    }

    if (authorId && !authorDetailLoaded) {
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
                {authorDetail
                    ? "Cập nhật thông tin tác giả"
                    : "Thêm mới tác giả"}
            </Typography>
            <form onSubmit={handleSubmit(handleSubmitData)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="fullName"
                            label="Tên đầy đủ"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="country"
                            label="Quốc tịch"
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <AppTextInput
                            multiline={true}
                            rows={4}
                            control={control}
                            name="biography"
                            label="Tiểu sử"
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
                                    src={authorDetail?.imageUrl}
                                    alt={authorDetail?.fullName}
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
                        {authorDetail ? "Cập nhật" : "Thêm mới"}
                    </LoadingButton>
                </Box>
            </form>
        </Box>
    );
}
