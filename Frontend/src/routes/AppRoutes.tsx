import { Routes, Route, Navigate } from "react-router-dom";

import SignIn from "../pages/auth/SignIn";
import SignUp from "../pages/auth/SignUp";

import Dashboard from "../pages/dashboard/Dashboard";
import AdminDashboard from "../pages/admin/AdminDashboard";
import UserDetails from "../pages/user/UserDetails";

import ProtectedRoute from "./ProtectedRoute";
import AdminRoute from "./AdminRoute";
import UserManagement from "../pages/admin/UserManagement";
import MainLayout from "../layouts/MainLayout";
import AdminLayout from "../layouts/AdminLayout";
import NotFound from "../pages/notfound/NotFound";
import Analysis from "../pages/admin/AiAnalysis";
import Roles from "../pages/admin/Roles";
import AgentWorkspace from "../pages/admin/AgentWorkspace";
import AIAudit from "../pages/admin/AIAudit";
import { ROUTE_PATHS } from "./routePaths";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/signin" replace />} />

      {/* Public */}
      <Route path="/signin" element={<SignIn />} />
      <Route path="/signup" element={<SignUp />} />

      {/* Protected */}
      <Route element={<ProtectedRoute />}>
        <Route element={<MainLayout />}>
          <Route
            path="/dashboard" element={<Dashboard />}
          />
        </Route>
      </Route>

      {/* Admin */}
      <Route element={<AdminRoute />}>
        <Route element={<AdminLayout />}>
          <Route path={ROUTE_PATHS.ADMIN} element={<AdminDashboard />} />
          <Route path={ROUTE_PATHS.ADMIN_USERS} element={<UserManagement />} />
          <Route path={ROUTE_PATHS.ADMIN_ROLES} element={<Roles />} />
          <Route path={ROUTE_PATHS.ADMIN_AI_ANALYSIS}element={<Analysis />} />
          <Route path={ROUTE_PATHS.USER_DETAILS}element={<UserDetails />} />
          <Route path={ROUTE_PATHS.ADMIN_AI_AUDIT}element={<AIAudit />} />
          <Route path={ROUTE_PATHS.AGENT_WORKSPACE} element={<AgentWorkspace />}/>
        </Route>
      </Route>

      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default AppRoutes;