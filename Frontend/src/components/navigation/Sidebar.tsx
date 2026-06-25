import { NavLink } from "react-router-dom";
import UserMenu from "./UserMenu";
import { ROUTE_PATHS } from "../../routes/routePaths";

type SidebarProps = {
  isOpen: boolean;
  onClose: () => void;
};

const Sidebar = ({
  isOpen,
  onClose,
}: SidebarProps) => {
  return (
    <>
      {/* Mobile Backdrop */}
      {isOpen && (
        <div
          className="fixed inset-0 z-40 bg-black/50 md:hidden"
          onClick={onClose}
        />
      )}

      <aside
        className={`
          fixed md:static
          inset-y-0 left-0 z-50
          w-64
          flex flex-col justify-between
          bg-slate-900 text-white
          transition-transform duration-300
          ${isOpen ? "translate-x-0" : "-translate-x-full"}
          md:translate-x-0
        `}
      >
        <div>
          <div className="p-6 text-xl font-bold">
            Admin Panel
          </div>

          <nav className="flex flex-col">
            <NavLink
              to={ROUTE_PATHS.ADMIN}
              onClick={onClose}
              className="px-6 py-3 hover:bg-slate-800"
            >
              Dashboard
            </NavLink>

            <NavLink
              to={ROUTE_PATHS.ADMIN_USERS}
              onClick={onClose}
              className="px-6 py-3 hover:bg-slate-800"
            >
              Users
            </NavLink>

            <NavLink
              to={ROUTE_PATHS.ADMIN_ROLES}
              onClick={onClose}
              className="px-6 py-3 hover:bg-slate-800"
            >
              Roles
            </NavLink>

            <NavLink
              to={ROUTE_PATHS.ADMIN_SETTINGS}
              onClick={onClose}
              className="px-6 py-3 hover:bg-slate-800"
            >
              Settings
            </NavLink>
            <NavLink to={ROUTE_PATHS.AGENT_WORKSPACE}
            onClick={onClose}
            className="px-6 py-3 hover:bg-slate-800">
              AI Agent
              
            </NavLink>
          </nav>
        </div>

        <div className="border-t border-slate-700 p-4">
          <UserMenu dark />
        </div>
      </aside>
    </>
  );
};

export default Sidebar;