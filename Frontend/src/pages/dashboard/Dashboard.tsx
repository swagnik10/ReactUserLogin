import { useAppSelector } from "../../app/hooks";
import AiFunSection from "../../components/ai-fun/AiFunSection";

const Dashboard = () => {
  const user = useAppSelector(
    (state) => state.auth.user
  );

  return (
    <div>
      <h1 className="text-3xl font-bold">
        Welcome {user?.firstName}
      </h1>

      <div className="mt-6 grid gap-4 md:grid-cols-3">
        <div className="rounded-lg bg-white p-4 shadow">
          <h2 className="font-semibold">
            Profile
          </h2>

          <p>{user?.emailId}</p>
        </div>

        <div className="rounded-lg bg-white p-4 shadow">
          <h2 className="font-semibold">
            Username
          </h2>

          <p>{user?.username}</p>
        </div>

        <div className="rounded-lg bg-white p-4 shadow">
          <h2 className="font-semibold">
            Role
          </h2>

          <p>{user?.role}</p>
        </div>
      </div>
       <AiFunSection />
    </div>
  );
};

export default Dashboard;