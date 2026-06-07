import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

import Card from "../../components/ui/Card";
import Input from "../../components/ui/Input";
import Button from "../../components/ui/Button";
import AuthLayout from "../../layouts/AuthLayout";

import {
  isValidEmail,
  isValidPassword,
  isValidUsername,
} from "../../utils/validators";
import { ROUTE_PATHS } from "../../routes/routePaths";
import { toast } from "sonner";

const SignUp = () => {
  const navigate = useNavigate();

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = () => {
    if (!firstName.trim()) {
      toast.error("First name is required");
      return;
    }

    if (!lastName.trim()) {
      toast.error("Last name is required");
      return;
    }

    if (!isValidUsername(username.trim())) {
      toast.error("Username must be at least 3 characters");
      return;
    }

    if (!isValidEmail(email.trim())) {
      toast.error("Invalid email");
      return;
    }

    if (!isValidPassword(password.trim())) {
      toast.error("Password must be at least 6 characters");
      return;
    }

    toast.success("Account created successfully");

    navigate(ROUTE_PATHS.SIGN_IN);
  };

  return (
    <AuthLayout>  
      <Card>
        <h1 className="mb-6 text-center text-2xl font-bold">
          Welcome Aboard
        </h1>

        <div className="space-y-4">
          <Input
            label="First Name"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />

          <Input
            label="Last Name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />

          <Input
            label="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />

          <Input
            label="Email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <Input
            label="Password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          
          <Button onClick={handleSubmit} fullWidth>
            Create Account
          </Button>

          <p className="text-center text-sm">
            Already have an account?{" "}
            <Link
              to={ROUTE_PATHS.SIGN_IN}
              className="text-blue-600 hover:underline"
            >
              Sign In
            </Link>
          </p>
        </div>
      </Card>
    </AuthLayout>
  );
};

export default SignUp;