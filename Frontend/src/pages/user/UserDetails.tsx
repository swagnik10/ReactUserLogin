import { useParams } from "react-router-dom";

const UserDetails = () => {
  const { id } = useParams();

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold">User Details</h1>
      <p>User ID: {id}</p>
    </div>
  );
};

export default UserDetails;