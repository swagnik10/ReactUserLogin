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

  const USERS_PER_PAGE = 6;
  const [currentPage, setCurrentPage] = useState(1);

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

  const totalPages = Math.ceil(
    filteredUsers.length / USERS_PER_PAGE
  );

  const startIndex = (currentPage - 1) * USERS_PER_PAGE;
  const endIndex = startIndex + USERS_PER_PAGE;

  const paginatedUsers = filteredUsers.slice(
    startIndex,
    endIndex
  );

  const getRoleBadgeClass = (role: string) => {
    switch (role) {
      case "SystemAdministrator":
        return "bg-red-100 text-red-700";

      case "Admin":
        return "bg-purple-100 text-purple-700";

      case "PowerUser":
        return "bg-amber-100 text-amber-700";

      case "User":
        return "bg-green-100 text-green-700";

      default:
        return "bg-slate-100 text-slate-700";
    }
  };

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
                  onChange={(e) => {
                    setSearchTerm(e.target.value);
                    setCurrentPage(1);
                  }}
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

                    paginatedUsers.map((user) => (
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
                            className={`inline-flex rounded-full px-3 py-1 text-xs font-semibold ${getRoleBadgeClass(
                              user.role
                            )}`}
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

          {totalPages > 1 && (
            <div className="mt-6 flex items-center justify-between">
              <button
                onClick={() =>
                  setCurrentPage((prev) => Math.max(prev - 1, 1))
                }
                disabled={currentPage === 1}
                className="rounded-md border px-4 py-2 text-sm font-medium disabled:cursor-not-allowed disabled:opacity-50 hover:bg-slate-100"
              >
                Previous
              </button>

              <div className="flex items-center gap-2">
                {Array.from({ length: totalPages }, (_, index) => {
                  const page = index + 1;

                  return (
                    <button
                      key={page}
                      onClick={() => setCurrentPage(page)}
                      className={`h-9 w-9 rounded-md text-sm font-medium transition
              ${currentPage === page
                          ? "bg-blue-600 text-white"
                          : "border hover:bg-slate-100"
                        }`}
                    >
                      {page}
                    </button>
                  );
                })}
              </div>

              <button
                onClick={() =>
                  setCurrentPage((prev) =>
                    Math.min(prev + 1, totalPages)
                  )
                }
                disabled={currentPage === totalPages}
                className="rounded-md border px-4 py-2 text-sm font-medium disabled:cursor-not-allowed disabled:opacity-50 hover:bg-slate-100"
              >
                Next
              </button>
            </div>
          )}


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