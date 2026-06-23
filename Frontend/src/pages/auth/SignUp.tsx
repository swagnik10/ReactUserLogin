import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

import Card from "../../components/ui/Card";
import Input from "../../components/ui/Input";
import Button from "../../components/ui/Button";
import AuthLayout from "../../layouts/AuthLayout";

import {
  isValidEmail,
  isValidPassword,
  isValidPhoneNumber,
  isValidUsername,
} from "../../utils/validators";
import { ROUTE_PATHS } from "../../routes/routePaths";
import { toast } from "sonner";
import { register } from "../../services/auth/authService";

const SignUp = () => {
  const navigate = useNavigate();

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async () => {

    try {
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

      if (!isValidPhoneNumber(phoneNumber.trim())) {
        toast.error("Phone Number Should be 10 digits long");
        return;
      }
      setIsLoading(true);
      await register({
        username,
        firstName,
        lastName,
        email,
        phoneNumber,
        password,
      });

      toast.success("Account created successfully");

      navigate(ROUTE_PATHS.SIGN_IN);
    }
    catch (error: any) {
      toast.error(
        error.message ??
        "Registration failed"
      );
    }
    finally {
      setIsLoading(false);
    }
    ;
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
            label="Phone Number"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
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

          <Button onClick={handleSubmit} fullWidth disabled={isLoading}>
            Create Account
          </Button>

          <p className="text-center text-sm">
            Already have an account?{" "}
            {isLoading ? (
              <span className="text-gray-400">Sign In</span>
            ) : (
              <Link
                to={ROUTE_PATHS.SIGN_IN}
                className="text-blue-600 hover:underline"
              >
                Sign In
              </Link>
            )}
          </p>
        </div>
      </Card>
    </AuthLayout>
  );
};

export default SignUp;