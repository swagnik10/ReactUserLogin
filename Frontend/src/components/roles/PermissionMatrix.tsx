import type { RoleDto } from "../../types/role";

interface Props {
    role: RoleDto | null;
}

export default function PermissionMatrix({ role }: Props) {
    if (!role) {
        return (
            <div className="flex min-h-[350px] items-center justify-center rounded-xl border-2 border-dashed border-gray-300 p-6">
                <div className="text-center">
                    <h2 className="text-xl font-semibold">
                        Select a Role
                    </h2>

                    <p className="mt-2 text-sm text-gray-500">
                        Choose a role from the list to view its permissions.
                    </p>
                </div>
            </div>
        );
    }

    const grouped = role.permissions.reduce((groups, permission) => {
        if (!groups[permission.category]) {
            groups[permission.category] = [];
        }

        groups[permission.category].push(permission);

        return groups;
    }, {} as Record<string, typeof role.permissions>);

    return (
        <div className="rounded-xl border bg-white p-5 shadow-sm">
            <div className="mb-6 border-b pb-5">
                <h2 className="text-2xl font-semibold">
                    {role.name}
                </h2>

                <p className="mt-2 text-sm leading-6 text-gray-600">
                    {role.description}
                </p>

                <div className="mt-4 inline-flex rounded-full bg-blue-50 px-3 py-1 text-sm font-medium text-blue-700">
                    {
                        role.permissions.filter(p => p.granted).length
                    }{" "}
                    Granted Permissions
                </div>
            </div>

            <div className="space-y-6">
                {Object.entries(grouped).map(([category, permissions]) => (
                    <div key={category}>
                        <h3 className="mb-3 border-b pb-2 text-lg font-bold">
                            {category}
                        </h3>

                        <div className="space-y-2">
                            {permissions.map(permission => (
                                <div
                                    key={permission.name}
                                    className="flex items-center justify-between rounded-lg border p-3"
                                >
                                    <span className="text-sm sm:text-base">
                                        {permission.name.split(".")[1]}
                                    </span>

                                    <span className="text-xl">
                                        {permission.granted ? "✅" : "❌"}
                                    </span>
                                </div>
                            ))}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}