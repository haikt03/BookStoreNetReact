import { Container, Grid, Paper, Typography, Box } from "@mui/material";
import Slider from "react-slick";
import { Fade, Slide, Zoom } from "react-awesome-reveal";

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
        <Container sx={{ my: 5 }}>
            <Grid container spacing={4} justifyContent="center">
                <Grid item xs={12}>
                    <Slider {...settings}>
                        {[1, 2, 3, 4].map((index) => (
                            <div key={index} style={slideShowStyle}>
                                <img
                                    src={`/images/slideshow_${index}.jpg`}
                                    alt="hero"
                                    style={{
                                        width: "100%",
                                        height: "100%",
                                    }}
                                />
                            </div>
                        ))}
                    </Slider>
                </Grid>
            </Grid>

            <Grid container spacing={4} justifyContent="center" sx={{ mt: 5 }}>
                <Grid item xs={12} md={6}>
                    <Slide direction="left" triggerOnce>
                        <Paper
                            elevation={3}
                            sx={{
                                p: 4,
                                height: "100%",
                                display: "flex",
                                flexDirection: "column",
                                justifyContent: "center",
                                alignItems: "center",
                            }}
                        >
                            <Typography
                                variant="h5"
                                gutterBottom
                                align="center"
                            >
                                Giới Thiệu Về{" "}
                                <span
                                    style={{
                                        color: "#1976d2",
                                        fontWeight: "bold",
                                    }}
                                >
                                    Book Corner
                                </span>
                            </Typography>
                            <Typography
                                variant="body1"
                                color="textSecondary"
                                paragraph
                                sx={{ textAlign: "justify" }}
                            >
                                <span
                                    style={{
                                        color: "#1976d2",
                                        fontWeight: "bold",
                                    }}
                                >
                                    Book Corner
                                </span>{" "}
                                là một địa điểm mua sách uy tín, nơi mà bạn có
                                thể tìm thấy các cuốn sách phong phú từ nhiều
                                thể loại. Chúng tôi cam kết mang lại trải nghiệm
                                mua sắm dễ dàng, nhanh chóng với dịch vụ hỗ trợ
                                khách hàng tận tình.
                                <br />
                                Không chỉ là nơi mua sắm, chúng tôi còn là cầu
                                nối giữa bạn và những tri thức tuyệt vời. Đội
                                ngũ của chúng tôi luôn sẵn sàng lắng nghe và
                                phục vụ bạn với sự tận tâm, mang đến sự hài lòng
                                cao nhất cho từng khách hàng.
                            </Typography>
                        </Paper>
                    </Slide>
                </Grid>

                <Grid item xs={12} md={6}>
                    <Box sx={{ position: "relative" }}>
                        <Zoom triggerOnce>
                            <img
                                src="/images/bookstore.jpg"
                                alt="Bookstore"
                                style={{
                                    width: "100%",
                                    height: "auto",
                                    borderRadius: "8px",
                                    objectFit: "cover",
                                    maxHeight: "100%",
                                }}
                            />
                        </Zoom>
                    </Box>
                </Grid>
            </Grid>

            <Grid container spacing={4} justifyContent="center" sx={{ mt: 5 }}>
                <Grid item xs={12}>
                    <Typography variant="h5" align="center" gutterBottom>
                        Sách Nổi Bật
                    </Typography>
                </Grid>
                {[
                    { name: "10 Kỹ Năng Sinh Tồn", image: "book_1.jpg" },
                    {
                        name: "1000 Phát Minh & Khám Phá Vĩ Đại",
                        image: "book_2.jpg",
                    },
                    { name: "1000+ Little Things", image: "book_3.jpg" },
                ].map((book, index) => (
                    <Grid item xs={12} sm={4} key={index}>
                        <Zoom triggerOnce>
                            <Paper
                                elevation={3}
                                sx={{ p: 3, textAlign: "center" }}
                            >
                                <img
                                    src={`/images/${book.image}`}
                                    alt={`book_${index}`}
                                    style={{
                                        width: "100%",
                                        height: "auto",
                                        maxHeight: 250,
                                        objectFit: "cover",
                                        borderRadius: 8,
                                    }}
                                />
                                <Typography variant="h6" sx={{ mt: 2 }}>
                                    {book.name}
                                </Typography>
                                <Typography
                                    variant="body2"
                                    color="textSecondary"
                                >
                                    Lorem Ipsum is simply dummy text of the
                                    printing and typesetting industry. Lorem
                                    Ipsum has been the industry's standard dummy
                                    text ever since the 1500s, when an unknown
                                    printer took a galley of type and scrambled
                                    it to make a type specimen book.
                                </Typography>
                            </Paper>
                        </Zoom>
                    </Grid>
                ))}
            </Grid>

            <Grid container spacing={4} justifyContent="center" sx={{ mt: 5 }}>
                <Grid item xs={12}>
                    <Typography variant="h5" align="center" gutterBottom>
                        Tác Giả Nổi Bật
                    </Typography>
                </Grid>
                {[
                    { name: "Adam Bray", image: "author_1.jpg" },
                    { name: "Adam Grant", image: "author_2.jpg" },
                    { name: "Alicia Vu", image: "author_3.jpg" },
                ].map((author, index) => (
                    <Grid item xs={12} sm={4} key={index}>
                        <Zoom triggerOnce>
                            <Paper
                                elevation={3}
                                sx={{ p: 3, textAlign: "center" }}
                            >
                                <img
                                    src={`/images/${author.image}`}
                                    alt={author.name}
                                    style={{
                                        width: "100%",
                                        height: "auto",
                                        maxHeight: 200,
                                        objectFit: "cover",
                                        borderRadius: 8,
                                    }}
                                />
                                <Typography variant="h6" sx={{ mt: 2 }}>
                                    {author.name}
                                </Typography>
                                <Typography
                                    variant="body2"
                                    color="textSecondary"
                                >
                                    Lorem Ipsum is simply dummy text of the
                                    printing and typesetting industry. Lorem
                                    Ipsum has been the industry's standard dummy
                                    text ever since the 1500s, when an unknown
                                    printer took a galley of type and scrambled
                                    it to make a type specimen book.
                                </Typography>
                            </Paper>
                        </Zoom>
                    </Grid>
                ))}
            </Grid>

            <Grid container spacing={4} justifyContent="center" sx={{ mt: 5 }}>
                <Grid item xs={12}>
                    <Typography variant="h5" align="center" gutterBottom>
                        Đánh Giá Của Khách Hàng
                    </Typography>
                </Grid>
                {[
                    {
                        name: "Ronaldo",
                        image: "reviewer_1.jpg",
                        review: "Sản phẩm tuyệt vời! Tôi sẽ quay lại mua tiếp.",
                    },
                    {
                        name: "Messi",
                        image: "reviewer_2.jpg",
                        review: "Dịch vụ rất tận tâm và chu đáo. Tôi rất hài lòng!",
                    },
                    {
                        name: "Lord Bendtner",
                        image: "reviewer_3.jpg",
                        review: "Sách chất lượng cao và đóng gói kỹ lưỡng. Đánh giá 5 sao!",
                    },
                ].map((reviewer, index) => (
                    <Grid item xs={12} sm={4} key={index}>
                        <Fade direction="up" triggerOnce>
                            <Paper
                                elevation={2}
                                sx={{ p: 3, textAlign: "center" }}
                            >
                                <img
                                    src={`/images/${reviewer.image}`}
                                    alt={reviewer.name}
                                    style={{
                                        width: 80,
                                        height: 80,
                                        borderRadius: "50%",
                                        marginBottom: 16,
                                    }}
                                />
                                <Typography
                                    variant="body2"
                                    color="textSecondary"
                                    fontStyle="italic"
                                >
                                    "{reviewer.review}"
                                </Typography>
                                <Typography variant="h6" sx={{ mt: 2 }}>
                                    {reviewer.name}
                                </Typography>
                            </Paper>
                        </Fade>
                    </Grid>
                ))}
            </Grid>
        </Container>
    );
}
