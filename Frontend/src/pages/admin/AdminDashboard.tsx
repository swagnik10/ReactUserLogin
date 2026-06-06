import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

import { useAppDispatch } from "../../app/hooks";
import { logout } from "../../features/auth/authSlice";

const AdminDashboard = () => {
  const navigate = useNavigate();

  const dispatch = useAppDispatch();

  const handleLogout = () => {
    dispatch(logout());
    navigate("/signin");
  };

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold">Admin Dashboard</h1>

      <div className="mt-6 flex flex-col gap-2">
        <Link
          to="/user/101"
          className="text-blue-600 underline"
        >
          View User 101
          
        </Link>

        <Link
          to="/user/202"
          className="text-blue-600 underline"
        >
          View User 202
        </Link>

        
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

export default AdminDashboard;