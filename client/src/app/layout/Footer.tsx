import { Box, Paper, Typography, useTheme } from "@mui/material";

export default function Footer() {
    const theme = useTheme();

    return (
        <Paper
            sx={{
                backgroundColor: theme.palette.background.paper,
                color: theme.palette.text.primary,
                p: 3,
                mt: 5,
            }}
        >
            <Typography variant="h6" gutterBottom textAlign="center">
                Thông Tin Liên Hệ
            </Typography>
            <Box
                sx={{
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "space-around",
                }}
            >
                <Box>
                    <Typography variant="subtitle1">Địa chỉ:</Typography>
                    <Typography variant="body2" color="textSecondary">
                        55 Đ. Giải Phóng, Đồng Tâm, Hai Bà Trưng, Hà Nội
                    </Typography>
                </Box>
                <Box>
                    <Typography variant="subtitle1">Điện thoại:</Typography>
                    <Typography variant="body2" color="textSecondary">
                        (+84) 123 456 789
                    </Typography>
                </Box>
                <Box>
                    <Typography variant="subtitle1">Email:</Typography>
                    <Typography variant="body2" color="textSecondary">
                        support@bookcorner.com
                    </Typography>
                </Box>
                <Box>
                    <Typography variant="subtitle1">Giờ làm việc:</Typography>
                    <Typography variant="body2" color="textSecondary">
                        Thứ Hai - Thứ Sáu: 8:00 - 17:00
                    </Typography>
                </Box>
            </Box>
        </Paper>
    );
}
