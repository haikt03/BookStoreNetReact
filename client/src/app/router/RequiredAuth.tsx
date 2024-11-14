import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";
import { toast } from "react-toastify";

interface Props {
    roles?: string[];
}

export default function RequireAuth({ roles }: Props) {
    const { user, isAuthenticated } = useAppSelector((state) => state.account);
    const location = useLocation();

    if (!isAuthenticated) {
        toast.error("Bạn cần đăng nhập để có thể truy cập");
        return <Navigate to="/login" state={{ from: location }} />;
    }

    if (roles && !roles?.some((r) => user?.roles?.includes(r))) {
        toast.error("Bạn không có quyền truy cập trang này");
        return <Navigate to="/home" />;
    }

    return <Outlet />;
}
