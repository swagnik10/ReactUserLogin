import { Outlet } from "react-router-dom";
import Navbar from "../components/navigation/Navbar";

const MainLayout = () => {
  return (
    <div className="min-h-screen bg-gray-100">
      <Navbar />

      <main className="mx-auto max-w-7xl p-6">
        <Outlet />
      </main>
    </div>
  );
};

export default MainLayout;