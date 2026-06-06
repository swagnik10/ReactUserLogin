import { useNavigate } from "react-router-dom";

import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { logout } from "../../features/auth/authSlice";

const Dashboard = () => {
  const navigate = useNavigate();

  const dispatch = useAppDispatch();
  const user = useAppSelector((state) => state.auth.user);

  const handleLogout = () => {
    dispatch(logout());
    navigate("/signin");
  };

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold">Dashboard</h1>

      <div className="mt-4">
        <p>Name: {user?.firstName}</p>
        <p>Email: {user?.email}</p>
        <p>Role: {user?.role}</p>
      </div>

      <button
        onClick={handleLogout}
        className="mt-4 px-4 py-2 rounded bg-red-600 text-white"
      >
        Logout
      </button>
    </div>
  );
};

export default Dashboard;