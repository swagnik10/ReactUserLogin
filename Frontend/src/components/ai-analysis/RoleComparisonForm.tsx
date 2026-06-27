import type { RoleSummaryDto } from "../../features/auth/types/role";

type RoleComparisonFormProps = {
    roles: RoleSummaryDto[];
    roleA: string;
    roleB: string;
    loading: boolean;
    onRoleAChange: (value: string) => void;
    onRoleBChange: (value: string) => void;
    onCompare: () => void;
};

const RoleComparisonForm = ({
    roles,
    roleA,
    roleB,
    loading,
    onRoleAChange,
    onRoleBChange,
    onCompare,
}: RoleComparisonFormProps) => {
    const sameRole = roleA === roleB;

    return (
        <div className="bg-white rounded-lg border shadow-sm p-6">

            <div className="grid md:grid-cols-2 gap-6">

                <div>
                    <label className="block mb-2 font-medium">
                        Role A
                    </label>

                    <select
                        value={roleA}
                        disabled={loading}
                        onChange={(e) =>
                            onRoleAChange(e.target.value)
                        }
                        className="w-full border rounded-md p-2 disabled:cursor-not-allowed"
                    >
                        {roles.map((role) => (
                            <option
                                key={role.name}
                                value={role.name}
                            >
                                {role.name}
                            </option>
                        ))}
                    </select>
                </div>

                <div>
                    <label className="block mb-2 font-medium">
                        Role B
                    </label>

                    <select
                        value={roleB}
                        disabled={loading}
                        onChange={(e) =>
                            onRoleBChange(e.target.value)
                        }
                        className="w-full border rounded-md p-2 disabled:cursor-not-allowed"
                    >
                        {roles.map((role) => (
                            <option
                                key={role.name}
                                value={role.name}
                            >
                                {role.name}
                            </option>
                        ))}
                    </select>
                </div>

            </div>

            {sameRole && (
                <p className="mt-3 text-sm text-red-600">
                    Please select two different roles.
                </p>
            )}

            <div className="mt-6">
                <button
                    onClick={onCompare}
                    disabled={loading || sameRole}
                    className="
                        bg-blue-600
                        hover:bg-blue-700
                        text-white
                        px-6
                        py-2
                        rounded-md
                        disabled:opacity-50
                        disabled:cursor-not-allowed
                    "
                >
                    {loading
                        ? "Comparing..."
                        : "Compare Roles"}
                </button>
            </div>

        </div>
    );
};

export default RoleComparisonForm;