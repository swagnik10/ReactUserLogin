import { NavLink } from "react-router-dom";
import UserMenu from "./UserMenu";
import { ROUTE_PATHS } from "../../routes/routePaths";

const Sidebar = () => {
  return (
    <aside className="flex w-64 flex-col justify-between bg-slate-900 text-white">
      <div>
        <div className="p-6 text-xl font-bold">
          Admin Panel
        </div>

        <nav className="flex flex-col">
          <NavLink
            to={ROUTE_PATHS.ADMIN}
            className="px-6 py-3 hover:bg-slate-800"
          >
            Dashboard
          </NavLink>

          <NavLink
            to={ROUTE_PATHS.ADMIN_USERS}
            className="px-6 py-3 hover:bg-slate-800"
          >
            Users
          </NavLink>
          <NavLink 
            to={ROUTE_PATHS.ADMIN_ROLES}
            className="px-6 py-3 hover:bg-slate-800">
            Roles
          </NavLink>

          <NavLink 
            to={ROUTE_PATHS.ADMIN_SETTINGS}
            className="px-6 py-3 hover:bg-slate-800">
            Settings
          </NavLink>
        </nav>
      </div>

      <div className="border-t border-slate-700 p-4">
        <UserMenu dark={true} />
      </div>
    </aside>
  );
};

export default Sidebar;