import { Link } from "react-router-dom";
import { useEffect, useRef, useState } from "react";
import { getUsers } from "../../services/users/userService";
import type { User } from "../../features/auth/types/user";

const UserManagement = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const hasLoaded = useRef(false);
  useEffect(() => {
    if (hasLoaded.current) return;
    hasLoaded.current = true;

    const loadUsers = async () => {
      try {
        const data = await getUsers();
        setUsers(data);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    loadUsers();
  }, []);


  return (
    <>
      {loading ? <div>Loading users...</div> : (
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
              {users.map((user) => (
                <tr key={user.userId}>
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
                      to={`/user/${user.userId}`}
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

      )}
    </>
  );
};

export default UserManagement;