import { Link } from "react-router-dom";
import { ROUTE_PATHS } from "../../routes/routePaths";

const AdminDashboard = () => {

  return (
    <div className="p-6">
      <div className="p-6">
        <h1 className="text-2xl font-bold">
          Admin Dashboard
        </h1>

        <div className="mt-6">
          <Link
            to={ROUTE_PATHS.ADMIN_USERS}
            className="rounded bg-blue-600 px-4 py-2 text-white"
          >
            Manage Users
          </Link>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;