import { useAppSelector } from "../../app/hooks";
import LogoutButton from "./LogoutButton";

interface UserMenuProps {
    dark?: boolean;
}

const UserMenu = ({ dark = false }: UserMenuProps) => {
    const user = useAppSelector(
        (state) => state.auth.user
    );

    if (!user) {
        return null;
    }

    return (
        <div className="flex items-center gap-4">
            <div className="text-right">
                <p className="font-medium">
                    {user.firstName} {user.lastName}
                </p>

                <p
                    className={`text-sm ${dark
                        ? "text-slate-300"
                        : "text-gray-500"
                        }`}
                >
                    {user.role}
                </p>
            </div>

            <LogoutButton />
        </div>
    );
};

export default UserMenu;