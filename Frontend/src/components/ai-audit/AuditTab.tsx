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

            const result =
                await auditRbac();

            onResponse(result);

        }
        finally {

            onLoadingChange(false);

        }

    };

    return (

        <>

            <h4 className="mb-3">
                RBAC Security Audit
            </h4>

            <p className="text-muted">
                Run a complete AI-powered audit of every role,
                permission and security practice.
            </p>

            <button
                className="btn btn-primary mb-4"
                disabled={loading}
                onClick={handleAudit}
            >
                {loading
                    ? "Running Audit..."
                    : "Run Audit"}
            </button>

            {response && (

                <>

                    <div className="alert alert-warning">

                        <strong>
                            Overall Risk:
                        </strong>{" "}
                        {response.overallRisk}

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

                </>

            )}

        </>

    );

};

export default AuditTab;