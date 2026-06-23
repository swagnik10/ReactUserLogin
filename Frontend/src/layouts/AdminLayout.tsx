import { Outlet } from "react-router-dom";
import { useState } from "react";
import Sidebar from "../components/navigation/Sidebar";

const AdminLayout = () => {
  const [isSidebarOpen, setIsSidebarOpen] =
    useState(false);

  return (
    <div className="min-h-screen bg-gray-100">
      {/* Mobile Header */}
      <header className="flex items-center justify-between bg-slate-900 p-4 text-white md:hidden">
        <h1 className="text-lg font-bold">
          Admin Panel
        </h1>

        <button
          onClick={() =>
            setIsSidebarOpen((prev) => !prev)
          }
          className="text-2xl"
        >
          ☰
        </button>
      </header>

      <div className="min-h-screen md:flex">
        <Sidebar
          isOpen={isSidebarOpen}
          onClose={() => setIsSidebarOpen(false)}
        />

        <main className="flex-1 p-6 bg-gray-100 overflow-x-auto">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default AdminLayout;