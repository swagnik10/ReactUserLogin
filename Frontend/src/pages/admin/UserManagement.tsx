import { Link } from "react-router-dom";
import { mockUsers } from "../../data/mockUsers";

const UserManagement = () => {
  return (
    <div className="p-6">
      <h1 className="mb-6 text-2xl font-bold">
        User Management
      </h1>

      <table className="w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">Username</th>
            <th className="border p-2">Email</th>
            <th className="border p-2">Role</th>
            <th className="border p-2">Action</th>
          </tr>
        </thead>

        <tbody>
          {mockUsers.map((user) => (
            <tr key={user.id}>
              <td className="border p-2">
                {user.username}
              </td>

              <td className="border p-2">
                {user.email}
              </td>

              <td className="border p-2">
                {user.role}
              </td>

              <td className="border p-2">
                <Link
                  to={`/user/${user.id}`}
                  className="text-blue-600 hover:underline"
                >
                  View
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserManagement;