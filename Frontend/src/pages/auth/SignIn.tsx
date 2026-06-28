import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

import Input from "../../components/ui/Input";
import Button from "../../components/ui/Button";
import Card from "../../components/ui/Card";

import { useAppDispatch } from "../../app/hooks";
import { loginSuccess } from "../../features/auth/authSlice";

import { isValidEmail, isValidPassword } from "../../utils/validators";
import AuthLayout from "../../layouts/AuthLayout";
import { saveUser } from "../../features/auth/authStorage";
import { ROUTE_PATHS } from "../../routes/routePaths";
import { toast } from "sonner";
import { login } from "../../services/auth/authService";

const SignIn = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async () => {

    try {
      if (!isValidEmail(email.trim())) {
        toast.error("Please enter a valid email.");
        return;
      }
      if (!isValidPassword(password.trim())) {
        toast.error("Password must be at least 6 characters.");
        return;
      }
      setIsLoading(true);
      const response = await login({
        email,
        password,
      });

      const user = {
        userId: response.userId,
        username: response.username,
        firstName: response.firstName,
        lastName: response.lastName,
        role: response.role,
        emailId: response.emailId,
      };

      saveUser(user, response.token);

      localStorage.setItem("token", response.token);

      dispatch(loginSuccess(response));

      if (response.role === "Admin" || response.role === "SystemAdministrator") {
        navigate(ROUTE_PATHS.ADMIN);
      } else {
        navigate(ROUTE_PATHS.DASHBOARD);
      }

      toast.success("Login successful");
    } catch (error) {
      toast.error(error instanceof Error ? error.message : "Login failed");
    }
    finally {
      setIsLoading(false);
    }
  };

  return (
    <AuthLayout>
      <Card>
        <h1 className="mb-6 text-center text-2xl font-bold">
          Good to See You
        </h1>

        <div className="space-y-4">
          <Input
            label="Email"
            type="email"
            value={email}
            onChange={(e) =>
              setEmail(e.target.value)
            }
          />

          <Input
            label="Password"
            type="password"
            value={password}
            onChange={(e) =>
              setPassword(e.target.value)
            }
          />

          <Button onClick={handleSubmit} fullWidth disabled={isLoading}>
            Sign In
          </Button>

          <p className="text-center text-sm">
            Don't have an account?{" "}
            {isLoading ? (
              <span className="text-gray-400">Create Account</span>
            ) : (
              <Link
                to={ROUTE_PATHS.SIGN_UP}
                className="text-blue-600 hover:underline"
              >
                Create Account
              </Link>
            )}
          </p>
        </div>
      </Card>
    </AuthLayout>
  );
};

export default SignIn;