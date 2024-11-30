import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";
import { toast } from "react-toastify";
import { useEffect } from "react";

interface Props {
    requiredRole?: string;
}

export default function RequireAuth({ requiredRole }: Props) {
    const { user, role } = useAppSelector((state) => state.account);
    const location = useLocation();

    useEffect(() => {
        if (!user) {
            toast.error("Bạn cần đăng nhập để có thể truy cập");
        }
        if (requiredRole && requiredRole !== role) {
            toast.error("Bạn không có quyền truy cập trang này");
        }
    }, [user, role, requiredRole, location]);

    if (!user) {
        return <Navigate to="/login" state={{ from: location }} />;
    }

    if (requiredRole && requiredRole !== role) {
        return <Navigate to="/home" />;
    }

    return <Outlet />;
}
