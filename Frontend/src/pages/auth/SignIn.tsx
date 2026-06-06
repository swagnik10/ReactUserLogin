import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

import Input from "../../components/ui/Input";
import Button from "../../components/ui/Button";
import Card from "../../components/ui/Card";

import { useAppDispatch } from "../../app/hooks";
import { loginSuccess } from "../../features/auth/authSlice";

import { isValidEmail } from "../../utils/validators";

const SignIn = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [error, setError] = useState("");

  const handleSubmit = () => {
    setError("");

    if (!isValidEmail(email)) {
      setError("Please enter a valid email.");
      return;
    }

    if (!password) {
      setError("Password is required.");
      return;
    }

    dispatch(
      loginSuccess({
        id: "1",
        firstName: "Swagnik",
        lastName: "Roy",
        username: "swagnik",
        email,
        role: "user",
      })
    );

    navigate("/dashboard");
  };

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-100 p-4">
      <Card>
        <h1 className="mb-6 text-center text-2xl font-bold">
          Sign In
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
              to="/signup"
              className="text-blue-600"
            >
              Create Account
            </Link>
          </p>
        </div>
      </Card>
    </div>
  );
};

export default SignIn;