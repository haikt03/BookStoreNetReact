import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";
import { toast } from "react-toastify";

interface Props {
    role?: string;
}

export default function RequireAuth({ role }: Props) {
    const { user, isAuthenticated } = useAppSelector((state) => state.account);
    const location = useLocation();

    if (!isAuthenticated) {
        toast.error("Bạn cần đăng nhập để có thể truy cập");
        return <Navigate to="/login" state={{ from: location }} />;
    }

    if (role && !(role === user?.role)) {
        toast.error("Bạn không có quyền truy cập trang này");
        return <Navigate to="/home" />;
    }

    return <Outlet />;
}
