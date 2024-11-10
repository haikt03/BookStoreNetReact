import { Container, Grid, Paper, Typography } from "@mui/material";
import Slider from "react-slick";

const slideShowStyle = {
    display: "block",
    width: "100%",
    height: "auto",
    maxHeight: 500,
};

export default function Home() {
    const settings = {
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
    };

    return (
        <Container maxWidth="lg" sx={{ my: 5 }}>
            <Grid container spacing={4} justifyContent="center">
                <Grid item xs={12}>
                    <Slider {...settings}>
                        {[1, 2, 3, 4].map((index) => (
                            <div key={index} style={slideShowStyle}>
                                <img
                                    src={`/images/slideshow_${index}.jpg`}
                                    alt="hero"
                                    style={{ width: "100%", height: "100%" }}
                                />
                            </div>
                        ))}
                    </Slider>
                </Grid>
            </Grid>

            <Grid container spacing={4} justifyContent="center" sx={{ mt: 5 }}>
                <Grid item xs={12}>
                    <Paper elevation={3} sx={{ p: 4 }}>
                        <Typography variant="h6" gutterBottom>
                            Giới Thiệu Về{" "}
                            <span
                                style={{ color: "#1976d2", fontWeight: "bold" }}
                            >
                                Book Conner
                            </span>
                        </Typography>
                        <Typography
                            variant="body1"
                            color="textSecondary"
                            paragraph
                        >
                            <span
                                style={{ color: "#1976d2", fontWeight: "bold" }}
                            >
                                Book Conner
                            </span>{" "}
                            là một địa điểm mua sách uy tín, nơi mà bạn có thể
                            tìm thấy các cuốn sách phong phú từ nhiều thể loại.
                            Chúng tôi cam kết mang lại trải nghiệm mua sắm dễ
                            dàng, nhanh chóng với dịch vụ hỗ trợ khách hàng tận
                            tình.
                        </Typography>
                        <Typography
                            variant="body1"
                            color="textSecondary"
                            paragraph
                        >
                            Đội ngũ{" "}
                            <span
                                style={{ color: "#1976d2", fontWeight: "bold" }}
                            >
                                Book Conner
                            </span>{" "}
                            luôn sẵn sàng cung cấp những đầu sách hay nhất với
                            giá cả hợp lý. Dù bạn đang tìm kiếm một cuốn sách
                            mới để đọc, hay đang tìm quà tặng ý nghĩa, chúng tôi
                            đều có thể đáp ứng nhu cầu của bạn.
                        </Typography>
                    </Paper>
                </Grid>
            </Grid>

            <Typography variant="h6" gutterBottom sx={{ mt: 5 }}>
                Thông Tin Liên Hệ
            </Typography>
            <Grid container spacing={3}>
                <Grid item xs={12} sm={6} md={3}>
                    <Typography variant="subtitle1">Địa chỉ:</Typography>
                    <Typography variant="body2" color="textSecondary">
                        55 Đ. Giải Phóng, Đồng Tâm, Hai Bà Trưng, Hà Nội
                    </Typography>
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
                    <Typography variant="subtitle1">Điện thoại:</Typography>
                    <Typography variant="body2" color="textSecondary">
                        (+84) 123 456 789
                    </Typography>
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
                    <Typography variant="subtitle1">Email:</Typography>
                    <Typography variant="body2" color="textSecondary">
                        support@bookconner.com
                    </Typography>
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
                    <Typography variant="subtitle1">Giờ làm việc:</Typography>
                    <Typography variant="body2" color="textSecondary">
                        Thứ Hai - Thứ Sáu: 8:00 - 17:00
                    </Typography>
                </Grid>
            </Grid>
        </Container>
    );
}
