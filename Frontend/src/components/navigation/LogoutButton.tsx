import { useNavigate } from "react-router-dom";

import { useAppDispatch } from "../../app/hooks";
import { logout } from "../../features/auth/authSlice";
import { clearStoredUser } from "../../features/auth/authStorage";
import { ROUTE_PATHS } from "../../routes/routePaths";
import Button from "../ui/Button";

const LogoutButton = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleLogout = () => {
    clearStoredUser();

    dispatch(logout());

    navigate(ROUTE_PATHS.SIGN_IN);
  };

  return (
    <Button variant="danger" onClick={handleLogout}>
      Logout
    </Button>
  );
};

export default LogoutButton;