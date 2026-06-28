import {
    auditRbac,
} from "../../services/role/roleService";

import type {
    RbacAuditDto,
} from "../../features/auth/types/role";

import AuditScoreCards from "./AuditScoreCards";
import RoleSummaryCards from "./RoleSummaryCards";
import FindingsList from "./FindingsList";
import RecommendationList from "./RecommendationList";
import { toast } from "sonner";

interface AuditTabProps {
    loading: boolean;

    response: RbacAuditDto | null;

    onLoadingChange: (loading: boolean) => void;

    onResponse: (
        response: RbacAuditDto
    ) => void;
}

const AuditTab = ({
    loading,
    response,
    onLoadingChange,
    onResponse,
}: AuditTabProps) => {

    const handleAudit = async () => {
        try {
            onLoadingChange(true);

            const result = await auditRbac();

            onResponse(result);
        }
        catch (error: any) {
            toast.error(
                error.message ??
                "RBAC audit ai failed"
            );
        }
        finally {
            onLoadingChange(false);
        }
    };

    return (
        <div className="space-y-6">

            <div>
                <h2 className="text-xl font-semibold text-gray-900">
                    RBAC Security Audit
                </h2>

                <p className="mt-2 text-sm text-gray-600">
                    Run a complete AI-powered audit of every role,
                    permission, and security practice.
                </p>
            </div>

            <div>
                <button
                    type="button"
                    disabled={loading}
                    onClick={handleAudit}
                    className="rounded-lg bg-blue-600 px-5 py-2.5 text-sm font-medium text-white shadow transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400"
                >
                    {loading
                        ? "Running Audit..."
                        : "Run Audit"}
                </button>
            </div>

            {response && (
                <div className="space-y-6">

                    <div className="rounded-lg border border-yellow-200 bg-yellow-50 px-4 py-3">
                        <div className="flex items-center gap-2">
                            <span className="font-semibold text-yellow-800">
                                Overall Risk:
                            </span>

                            <span className="text-yellow-900">
                                {response.overallRisk}
                            </span>
                        </div>
                    </div>

                    <AuditScoreCards
                        score={response.score}
                    />

                    <RoleSummaryCards
                        bestDesignedRole={response.bestDesignedRole}
                        mostPrivilegedRole={response.mostPrivilegedRole}
                        mostRestrictedRole={response.mostRestrictedRole}
                    />

                    <FindingsList
                        findings={response.findings}
                    />

                    <RecommendationList
                        recommendations={response.recommendations}
                    />

                </div>
            )}

        </div>
    );
};

export default AuditTab;