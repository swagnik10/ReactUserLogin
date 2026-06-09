import { Navigate, Outlet } from "react-router-dom";
import { useAppSelector } from "../app/hooks";

const ProtectedRoute = () => {
   const { isAuthenticated, token } = useAppSelector(
    (state) => state.auth
  );

  const isValidAuth = isAuthenticated && !!token?.trim();

  return isValidAuth ? <Outlet /> : <Navigate to="/signin" replace />;
};

export default ProtectedRoute;