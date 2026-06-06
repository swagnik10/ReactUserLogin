import { Link } from "react-router-dom";
import { ROUTE_PATHS } from "../../routes/routePaths";

const NotFound = () => {
  return (
    <div className="flex min-h-screen flex-col items-center justify-center">
      <h1 className="text-6xl font-bold">
        404
      </h1>

      <p className="mt-4 text-gray-500">
        Page not found
      </p>

      <Link
        to={ROUTE_PATHS.SIGN_IN}
        className="mt-6 rounded bg-blue-600 px-4 py-2 text-white"
      >
        Go Home
      </Link>
    </div>
  );
};

export default NotFound;