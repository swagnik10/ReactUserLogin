import UserMenu from "./UserMenu";

const Navbar = () => {
  return (
    <nav className="flex items-center justify-between bg-white px-6 py-4 shadow">
      <h1 className="text-xl font-bold">
        Login Portal
      </h1>

      <UserMenu />
    </nav>
  );
};

export default Navbar;