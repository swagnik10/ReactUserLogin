import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { getUserById } from "../../services/users/userService";
import type { UserInformation } from "../../features/auth/types/user";


const UserDetails = () => {
  const { id } = useParams();
  const [user, setUser] = useState<UserInformation | null>(null);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    const loadUser = async () => {
      try {
        if (!id) return;

        const data = await getUserById(Number(id));
        setUser(data);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    loadUser();
  }, [id]);

  if (!user) {
    return <div>User not found</div>;
  }
  if (loading) {
    return <div>Loading user...</div>;
  }

  return (
    <>
      <div className="p-6">
        <h1 className="text-2xl font-bold">User Details</h1>
        <p>
          <strong>First Name:</strong>{" "}
          {user.firstName}
        </p>

        <p>
          <strong>Last Name:</strong>{" "}
          {user.lastName}
        </p>

        <p>
          <strong>Username:</strong>{" "}
          {user.username}
        </p>

        <p>
          <strong>Email:</strong>{" "}
          {user.email}
        </p>

        <p>
          <strong>Role:</strong>{" "}
          {user.role}
        </p>
      </div>
    </>

  );
};

export default UserDetails;