import { Link } from "react-router-dom";
import { useEffect, useRef, useState } from "react";
import { deleteUser, getUsers } from "../../services/users/userService";
import type { User } from "../../features/auth/types/user";
import { toast } from "sonner";
import ConfirmDialog from "./ConfirmDialog";

const UserManagement = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const [userToDelete, setUserToDelete] = useState<number | null>(null);
  const [searchTerm, setSearchTerm] = useState("");
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

  const handleDelete = async (
    userId: number
  ) => {
    try {
      await deleteUser(userId);

      setUsers((prev) =>
        prev.filter(
          (user) => user.userId !== userId
        )
      );

      toast.success(
        "User deleted successfully"
      );
    } catch (error) {
      console.error(error);

      toast.error(
        "Failed to delete user"
      );
    } finally {
      setUserToDelete(null);
    }
  };

  const filteredUsers = users.filter((user) =>
    user.username
      .toLowerCase()
      .includes(searchTerm.toLowerCase())
  );

  return (
    <>
      {loading ? <div>Loading users...</div> : (
        <div className="p-6">
          <div className="mb-6 flex flex-col gap-4 lg:flex-row lg:items-center lg:justify-between">
            <div>
              <h1 className="text-3xl font-bold text-slate-900">
                User Management
              </h1>

              <p className="mt-1 text-sm text-slate-500">
                Manage users and permissions.
              </p>
            </div>

            <div className="flex flex-col gap-3 sm:flex-row sm:items-center">
              {/* Search */}
              <div className="relative">
                <input
                  type="text"
                  placeholder="Search users..."
                  value={searchTerm}
                  onChange={(e) =>
                    setSearchTerm(e.target.value)
                  }
                  className="w-full rounded-lg border border-slate-300 bg-white py-2 pl-10 pr-4 focus:border-blue-500 focus:outline-none sm:w-72"
                />

                <span className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400">
                  🔍
                </span>
              </div>

              {/* User Count */}
              <div className="rounded-lg bg-white px-4 py-2 shadow-sm border">
                <div className="text-xs uppercase tracking-wide text-slate-500">
                  Users
                </div>

                <div className="text-xl font-bold text-slate-900">
                  {filteredUsers.length}
                </div>
              </div>
            </div>
          </div>

          <div className="overflow-hidden rounded-xl bg-white shadow">
            <div className="overflow-x-auto">
              <table className="min-w-full">
                <thead className="bg-slate-50">
                  <tr>
                    <th className="px-6 py-4 text-left text-sm font-semibold text-slate-600">
                      Username
                    </th>

                    <th className="px-6 py-4 text-left text-sm font-semibold text-slate-600">
                      Email
                    </th>

                    <th className="px-6 py-4 text-left text-sm font-semibold text-slate-600">
                      Role
                    </th>

                    <th className="px-6 py-4 text-center text-sm font-semibold text-slate-600">
                      Actions
                    </th>
                  </tr>
                </thead>

                <tbody className="divide-y divide-slate-200">
                  {filteredUsers.length > 0 ? (

                    filteredUsers.map((user) => (
                      <tr
                        key={user.userId}
                        className="hover:bg-slate-50 transition-colors"
                      >
                        <td className="px-6 py-4 font-medium text-slate-900">
                          {user.username}
                        </td>

                        <td className="px-6 py-4 text-slate-600">
                          {user.email}
                        </td>

                        <td className="px-6 py-4">
                          <span
                            className={`inline-flex rounded-full px-3 py-1 text-xs font-semibold
                ${user.role === "Admin" ||
                                user.role === "DemoAdmin"
                                ? "bg-purple-100 text-purple-700"
                                : "bg-green-100 text-green-700"
                              }`}
                          >
                            {user.role}
                          </span>
                        </td>

                        <td className="px-6 py-4">
                          <div className="flex justify-center gap-2">
                            <Link
                              to={`/user/${user.userId}`}
                              className="rounded-md bg-blue-50 px-3 py-1.5 text-sm font-medium text-blue-600 hover:bg-blue-100"
                            >
                              View
                            </Link>

                            <button
                              onClick={() =>
                                setUserToDelete(user.userId)
                              }
                              className="rounded-md bg-red-50 px-3 py-1.5 text-sm font-medium text-red-600 hover:bg-red-100"
                            >
                              Delete
                            </button>
                          </div>
                        </td>
                      </tr>
                    ))) : (<tr>
                      <td
                        colSpan={4}
                        className="py-8 text-center text-slate-500"
                      >
                        No users found.
                      </td>
                    </tr>)}
                </tbody>
              </table>
            </div>
          </div>
          <ConfirmDialog
            isOpen={userToDelete !== null}
            title="Delete User"
            message="Are you sure you want to delete this user? This action cannot be undone."
            onCancel={() => setUserToDelete(null)}
            onConfirm={() => {
              if (userToDelete !== null) {
                handleDelete(userToDelete);
              }
            }}
          />
        </div>

      )}
    </>
  );
};

export default UserManagement;