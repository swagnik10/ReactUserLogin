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
import type { UserRole } from "../../features/auth/types";
import { ROUTE_PATHS } from "../../routes/routePaths";
import { toast } from "sonner";

const SignIn = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [error, setError] = useState("");

  const handleSubmit = () => {
    setError("");

    if (!isValidEmail(email.trim())) {
      toast.error("Please enter a valid email.");
      return;
    }

    if (!isValidPassword(password.trim())) {
      toast.error("Password must be at least 6 characters.");
      return;
    }

    const user = {
      id: "1",
      firstName: "Swagnik",
      lastName: "Ghosh",
      username: "swagnik10",
      email,
      role: "Admin" as UserRole
    };

    saveUser(user);

    dispatch(loginSuccess({ user, token: null }));
    
    if (user.role === "Admin") {
      navigate(ROUTE_PATHS.ADMIN);
    } else {
      navigate(ROUTE_PATHS.DASHBOARD);
    }
    toast.success("Login successful");
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

          {error && (
            <p className="text-red-500">
              {error}
            </p>
          )}

          <Button onClick={handleSubmit}>
            Sign In
          </Button>

          <p className="text-center text-sm">
            Don't have an account?{" "}
            <Link
              to={ROUTE_PATHS.SIGN_UP}
              className="text-blue-600"
            >
              Create Account
            </Link>
          </p>
        </div>
      </Card>
    </AuthLayout>
  );
};

export default SignIn;