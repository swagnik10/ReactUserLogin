import { useNavigate } from "react-router-dom";

import { useAppDispatch } from "../../app/hooks";
import { loginSuccess } from "../../features/auth/authSlice";

const SignIn = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleLogin = () => {
    dispatch(
      loginSuccess({
        id: "1",
        firstName: "Swagnik",
        lastName: "Ghosh",
        username: "swagnik",
        email: "admin@test.com",
        role: "admin",
      })
    );

    navigate("/dashboard");
  };

  return (
    <div className="flex flex-col items-center justify-center h-screen gap-4">
      <h1 className="text-2xl font-bold">Sign In</h1>

      <button
        onClick={handleLogin}
        className="px-4 py-2 bg-blue-600 text-white rounded"
      >
        Fake Login
      </button>
    </div>
  );
};

export default SignIn;