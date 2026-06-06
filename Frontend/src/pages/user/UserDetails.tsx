import { useParams } from "react-router-dom";
import { mockUsers } from "../../data/mockUsers";

const UserDetails = () => {
  const { id } = useParams();
  const user = mockUsers.find(
    (u) => u.id === id
  );

  if (!user) {
    return <div>User not found</div>;
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