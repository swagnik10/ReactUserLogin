import { useEffect, useState } from "react";
import {
    getRole,
    getRoles,
    analyzeRole,
} from "../../services/role/roleService";
import type {
    RoleDto,
    RoleSummaryDto,
    RoleAnalysisDto,
} from "../../features/auth/types/role";
import RoleList from "../../components/roles/RoleList";
import PermissionMatrix from "../../components/roles/PermissionMatrix";
import AiRoleAnalysis from "../../components/roles/AiRoleAnalysis";
import { toast } from "sonner";

const Roles = () => {
    const [roles, setRoles] = useState<RoleSummaryDto[]>([]);
    const [selectedRole, setSelectedRole] = useState<RoleDto | null>(null);
    const [analysis, setAnalysis] = useState<RoleAnalysisDto | null>(null);
    const [loadingAnalysis, setLoadingAnalysis] = useState(false);

    const [activeTab, setActiveTab] = useState<
        "permissions" | "analysis"
    >("permissions");

    useEffect(() => {
        loadRoles();
    }, []);

    async function loadRoles() {
        try {
            setSelectedRole(null);
            setAnalysis(null);

            const data = await getRoles();

            setRoles(data);
        }
        catch (error) {
            toast.error(error instanceof Error ? error.message : "Role Fetch failed");
        }
        finally {
            setSelectedRole(null);
            setAnalysis(null);
        }
    }

    async function loadRole(roleName: string) {
        try {
            setAnalysis(null);
            setActiveTab("permissions");

            const role = await getRole(roleName);

            setSelectedRole(role);
        }
        catch (error) {
            toast.error(error instanceof Error ? error.message : `Role ${roleName} Fetch failed`);
        }
        finally {
            setAnalysis(null);
            setActiveTab("permissions");
        }

    }

    async function analyzeSelectedRole() {
        if (!selectedRole) return;

        setLoadingAnalysis(true);

        try {
            const result = await analyzeRole(selectedRole.name);

            setAnalysis(result);
            setActiveTab("analysis");
        }
        catch (error) {
            toast.error(error instanceof Error ? error.message : `Ai analyze ${selectedRole.name} failed`);
        }
        finally {
            setLoadingAnalysis(false);
        }
    }

    return (
        <>
            <div className="mb-6">
                <h1 className="text-2xl font-bold">
                    Roles Management
                </h1>
            </div>

            <div className="grid grid-cols-1 gap-6 lg:grid-cols-4">
                <div className="lg:col-span-1">
                    <RoleList
                        roles={roles}
                        selectedRole={selectedRole?.name ?? ""}
                        loading={loadingAnalysis}
                        onSelect={loadRole}
                    />
                </div>

                <div className="lg:col-span-3">

                    {selectedRole && (
                        <div className="mb-6 border-b">
                            <nav className="flex gap-6">
                                <button
                                    disabled={loadingAnalysis}
                                    onClick={() =>
                                        setActiveTab("permissions")
                                    }
                                    className={`border-b-2 px-1 py-3 font-medium transition ${activeTab === "permissions"
                                            ? "border-blue-600 text-blue-600"
                                            : "border-transparent text-gray-500 hover:text-gray-700"
                                        }${loadingAnalysis
                                            ? "cursor-not-allowed opacity-50"
                                            : "cursor-pointer"
                                        }`}
                                >
                                    Permissions
                                </button>

                                <button
                                    disabled={loadingAnalysis}
                                    onClick={() => setActiveTab("analysis")}
                                    className={` border-b-2 px-1 py-3 font-medium transition ${activeTab === "analysis"
                                            ? "border-blue-600 text-blue-600"
                                            : "border-transparent text-gray-500 hover:text-gray-700"
                                        }${loadingAnalysis
                                            ? "cursor-not-allowed opacity-50"
                                            : "cursor-pointer"
                                        }`}
                                >
                                    AI Analysis
                                </button>
                            </nav>
                        </div>
                    )}

                    {activeTab === "permissions" && (
                        <PermissionMatrix role={selectedRole} />
                    )}

                    {selectedRole && activeTab === "analysis" && (
                        <AiRoleAnalysis
                            analysis={analysis}
                            loading={loadingAnalysis}
                            onAnalyze={analyzeSelectedRole}
                        />
                    )}
                </div>
            </div>
        </>
    );
};

export default Roles;