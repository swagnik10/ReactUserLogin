import { useNavigate } from "react-router-dom";

import { useAppDispatch } from "../../app/hooks";
import { logout } from "../../features/auth/authSlice";
import { clearStoredUser } from "../../features/auth/authStorage";
import { ROUTE_PATHS } from "../../routes/routePaths";

const LogoutButton = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleLogout = () => {
    clearStoredUser();

    dispatch(logout());

    navigate(ROUTE_PATHS.SIGN_IN);
  };

  return (
    <button
      onClick={handleLogout}
      className="rounded-md bg-red-500 px-4 py-2 text-sm text-white hover:bg-red-600"
    >
      Logout
    </button>
  );
};

export default LogoutButton;