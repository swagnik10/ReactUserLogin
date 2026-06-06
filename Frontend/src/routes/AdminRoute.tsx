import { Navigate, Outlet } from "react-router-dom";
import { useAppSelector } from "../app/hooks";

const AdminRoute = () => {
  const user = useAppSelector((state) => state.auth.user);

  if (!user) {
    return <Navigate to="/signin" replace />;
  }

  return user.role === "Admin" ? (
    <Outlet />
  ) : (
    <Navigate to="/dashboard" replace />
  );
};

export default AdminRoute;