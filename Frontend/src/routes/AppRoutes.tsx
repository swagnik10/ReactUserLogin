import { Routes, Route, Navigate } from "react-router-dom";

import SignIn from "../pages/auth/SignIn";
import SignUp from "../pages/auth/SignUp";
import Dashboard from "../pages/dashboard/Dashboard";
import AdminDashboard from "../pages/admin/AdminDashboard";
import UserDetails from "../pages/user/UserDetails";

import { ROUTE_PATHS } from "./routePaths";

const AppRoutes = () => {
  return (
    <Routes>
      {/* Default route → redirect to signin */}
      <Route path="/" element={<Navigate to={ROUTE_PATHS.SIGN_IN} replace />} />

      {/* Auth routes */}
      <Route path={ROUTE_PATHS.SIGN_IN} element={<SignIn />} />
      <Route path={ROUTE_PATHS.SIGN_UP} element={<SignUp />} />

      {/* App routes */}
      <Route path={ROUTE_PATHS.DASHBOARD} element={<Dashboard />} />
      <Route path={ROUTE_PATHS.ADMIN} element={<AdminDashboard />} />

      {/* Dynamic route */}
      <Route path={ROUTE_PATHS.USER_DETAILS} element={<UserDetails />} />

      {/* fallback */}
      <Route path="*" element={<div>404 - Page Not Found</div>} />
    </Routes>
  );
};

export default AppRoutes;