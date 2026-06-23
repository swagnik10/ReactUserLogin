import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { getUserById, updateUser, updateUserRole } from "../../services/users/userService";
import type { UserInformation } from "../../features/auth/types/user";
import { toast } from "sonner";



const UserDetails = () => {
  const { id } = useParams();
  const [user, setUser] = useState<UserInformation | null>(null);
  const [loading, setLoading] = useState(true);
  const [isEditing, setIsEditing] = useState(false);
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [isActive, setIsActive] = useState(false);
  const [role, setRole] = useState("");

  useEffect(() => {
    const loadUser = async () => {
      try {
        if (!id) return;
        const data = await getUserById(Number(id));
        setUser(data);
        setRole(data.role);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    loadUser();
  }, [id]);

  const handleUpdate = async () => {
    if (!id) return;

    try {
      await updateUser(Number(id), {
        firstName,
        lastName,
        email,
        phoneNumber,
        isActive,
      });

      setUser((prev) =>
        prev
          ? {
            ...prev,
            firstName,
            lastName,
            email,
            phoneNumber,
            isActive,
          }
          : null
      );

      setIsEditing(false);

      toast.success(
        "User updated successfully"
      );
    } catch (error) {
      toast.error(
        "Failed to update user"
      );
    }
  };

  const handleEdit = () => {
    if (!user) return;

    setFirstName(user.firstName);
    setLastName(user.lastName);
    setEmail(user.email);
    setPhoneNumber(user.phoneNumber);
    setIsActive(user.isActive);

    setIsEditing(true);
  };

  const handleCancel = () => {
    setIsEditing(false);
  };

  const handleRoleUpdate = async () => {
    if (!id) return;

    try {
      await updateUserRole(Number(id), {
        roleName: role,
      });

      setUser((prev) =>
        prev
          ? {
            ...prev,
            role,
          }
          : null
      );

      toast.success(
        "Role updated successfully"
      );
    } catch (error) {
      toast.error(
        "Failed to update role"
      );
    }
  };
  
  if (loading) {
    return <div>Loading user...</div>;
  }
  if (!user) {
    return <div>User not found</div>;
  }

  return (
    <>
      <div className="p-6 max-w-2xl rounded-lg border bg-white shadow">
        <h1 className="mb-6 text-2xl font-bold">
          User Details
        </h1>

        <div className="space-y-4">

          {/* First Name */}
          <div>
            <label className="mb-1 block font-medium">
              First Name
            </label>

            {isEditing ? (
              <input
                type="text"
                value={firstName}
                onChange={(e) =>
                  setFirstName(e.target.value)
                }
                className="w-full rounded-md border px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            ) : (
              <p>{user.firstName}</p>
            )}
          </div>

          {/* Last Name */}
          <div>
            <label className="mb-1 block font-medium">
              Last Name
            </label>

            {isEditing ? (
              <input
                type="text"
                value={lastName}
                onChange={(e) =>
                  setLastName(e.target.value)
                }
                className="w-full rounded-md border px-3 py-2"
              />
            ) : (
              <p>{user.lastName}</p>
            )}
          </div>

          {/* Username */}
          <div>
            <label className="mb-1 block font-medium">
              Username
            </label>
            <p>{user.username}</p>
          </div>

          <div>
            <label className="mb-1 block font-medium">
              Role
            </label>

            <div className="flex items-center gap-3">
              <select
                value={role}
                onChange={(e) =>
                  setRole(e.target.value)
                }
                className="rounded-md border px-3 py-2"
              >
                <option value="User">
                  User
                </option>

                <option value="Admin">
                  Admin
                </option>
              </select>

              <button
                onClick={handleRoleUpdate}
                className="rounded bg-purple-600 px-4 py-2 text-white hover:bg-purple-700 md:cursor-pointer"
              >
                Update Role
              </button>
            </div>
          </div>

          {/* Email */}
          <div>
            <label className="mb-1 block font-medium">
              Email
            </label>

            {isEditing ? (
              <input
                type="email"
                value={email}
                onChange={(e) =>
                  setEmail(e.target.value)
                }
                className="w-full rounded-md border px-3 py-2"
              />
            ) : (
              <p>{user.email}</p>
            )}
          </div>

          {/* Phone Number */}
          <div>
            <label className="mb-1 block font-medium">
              Phone Number
            </label>

            {isEditing ? (
              <input
                type="text"
                value={phoneNumber}
                onChange={(e) =>
                  setPhoneNumber(e.target.value)
                }
                className="w-full rounded-md border px-3 py-2"
              />
            ) : (
              <p>{user.phoneNumber}</p>
            )}
          </div>

          {/* Active */}
          <div className="flex items-center gap-2">
            <label className="font-medium">
              Active
            </label>

            {isEditing ? (
              <input
                type="checkbox"
                checked={isActive}
                onChange={(e) =>
                  setIsActive(e.target.checked)
                }
              />
            ) : (
              <span>
                {user.isActive ? "Yes" : "No"}
              </span>
            )}
          </div>

          {/* Buttons */}
          <div className="flex gap-3 pt-4">
            {!isEditing ? (
              <button
                onClick={handleEdit}
                className="rounded bg-blue-600 px-4 py-2 text-white hover:bg-blue-700 md:cursor-pointer"
              >
                Edit
              </button>
            ) : (
              <>
                <button
                  onClick={handleUpdate}
                  className="rounded bg-green-600 px-4 py-2 text-white hover:bg-green-700 md:cursor-pointer"
                >
                  Save
                </button>

                <button
                  onClick={handleCancel}
                  className="rounded bg-gray-500 px-4 py-2 text-white hover:bg-gray-600 md:cursor-pointer"
                >
                  Cancel
                </button>
              </>
            )}
          </div>
        </div>
      </div>
    </>

  );
};

export default UserDetails;