import type { RoleSummaryDto } from "../../types/role";

interface Props {
    roles: RoleSummaryDto[];
    selectedRole: string;
    onSelect(role: string): void;
}

export default function RoleList({
    roles,
    selectedRole,
    onSelect,
}: Props) {
    return (
        <div className="flex gap-3 overflow-x-auto pb-2 lg:block lg:space-y-3">
            {roles.map(role => (
                <button
                    key={role.name}
                    onClick={() => onSelect(role.name)}
                    className={`
                        min-w-[180px]
                        rounded-lg
                        border
                        p-4
                        text-left
                        transition
                        lg:w-full
                        ${selectedRole === role.name
                            ? "bg-blue-600 text-white border-blue-600"
                            : "bg-white hover:bg-gray-50"
                        }
                    `}
                >
                    <div className="font-semibold">
                        {role.name}
                    </div>

                    <p className={`mt-1 line-clamp-2 text-sm ${selectedRole === role.name
                            ? "text-blue-100"
                            : "text-gray-500"
                        }`}>
                        {role.description}
                    </p>

                    <div className={`mt-3 text-sm font-medium ${selectedRole === role.name
                            ? "text-blue-100"
                            : "text-gray-500"
                        }`}>
                        {role.permissionCount} Permissions
                    </div>
                </button>
            ))}
        </div>
    );
}