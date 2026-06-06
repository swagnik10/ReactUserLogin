import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";

import { useAppDispatch } from "../../app/hooks";
import { logout } from "../../features/auth/authSlice";

const UserDetails = () => {
  const { id } = useParams();

  const navigate = useNavigate();

  const dispatch = useAppDispatch();

  const handleLogout = () => {
    dispatch(logout());
    navigate("/signin");
  };

  return (
    <>
      <div className="p-6">
        <h1 className="text-2xl font-bold">User Details</h1>
        <p>User ID: {id}</p>
      </div>
      <button
        onClick={handleLogout}
        className="mt-4 px-4 py-2 rounded bg-red-600 text-white"
      >
        Logout
      </button>
    </>

  );
};

export default UserDetails;