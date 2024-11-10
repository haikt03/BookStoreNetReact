import { Container, Paper, Typography, Divider, Button } from "@mui/material";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Container component={Paper} sx={{ height: 400 }}>
            <Typography gutterBottom variant={"h3"}>
                Chúng tôi không thể tìm thấy trang bạn yêu cầu
            </Typography>
            <Divider />
            <Button component={Link} to="/catalog" fullWidth>
                Quay trở lại
            </Button>
        </Container>
    );
}
