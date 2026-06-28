import type { AuditFindingDto } from "../../features/auth/types/role";

interface FindingsListProps {
    findings: AuditFindingDto[];
}

const FindingsList = ({
    findings,
}: FindingsListProps) => {

    const getSeverityClasses = (severity: string) => {
        switch (severity.toLowerCase()) {
            case "critical":
                return "bg-red-100 text-red-800";
            case "high":
                return "bg-orange-100 text-orange-800";
            case "medium":
                return "bg-yellow-100 text-yellow-800";
            case "low":
                return "bg-green-100 text-green-800";
            default:
                return "bg-gray-100 text-gray-800";
        }
    };

    return (
        <div className="space-y-4">

            <h2 className="text-xl font-semibold text-gray-900">
                Findings
            </h2>

            {findings.map((finding, index) => (
                <div
                    key={index}
                    className="rounded-xl border border-gray-200 bg-white p-6 shadow-sm transition hover:shadow-md"
                >
                    <span
                        className={`inline-flex rounded-full px-3 py-1 text-xs font-semibold ${getSeverityClasses(
                            finding.severity
                        )}`}
                    >
                        {finding.severity}
                    </span>

                    <h3 className="mt-4 text-lg font-semibold text-gray-900">
                        {finding.title}
                    </h3>

                    <p className="mt-2 text-sm leading-6 text-gray-600">
                        {finding.description}
                    </p>
                </div>
            ))}

        </div>
    );
};

export default FindingsList;