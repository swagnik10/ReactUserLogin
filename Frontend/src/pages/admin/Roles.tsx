import { useEffect, useState } from "react";
import { getRole, getRoles, analyzeRole } from "../../services/role/roleService";
import type { RoleDto, RoleSummaryDto, RoleAnalysisDto } from "../../features/auth/types/role";
import RoleList from "../../components/roles/RoleList";
import PermissionMatrix from "../../components/roles/PermissionMatrix";
import AiRoleAnalysis from "../../components/roles/AiRoleAnalysis";

const Roles = () => {
    const [roles, setRoles] = useState<RoleSummaryDto[]>([]);
    const [selectedRole, setSelectedRole] = useState<RoleDto | null>(null);
    const [analysis, setAnalysis] = useState<RoleAnalysisDto | null>(null);
    const [loadingAnalysis, setLoadingAnalysis] = useState(false);

    useEffect(() => {
        loadRoles();
    }, []);

    async function loadRoles() {
        const data = await getRoles();

        setRoles(data);

        setAnalysis(null);

    }

    async function loadRole(roleName: string) {
        const role = await getRole(roleName);
        setSelectedRole(role);
    }

    async function analyzeSelectedRole() {
        if (!selectedRole) return;

        setLoadingAnalysis(true);

        try {
            const result =
                await analyzeRole(selectedRole.name);

            setAnalysis(result);
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
                        onSelect={loadRole}
                    />
                </div>

                <div className="lg:col-span-3">
                    <PermissionMatrix role={selectedRole} />

                    {selectedRole && (
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