import type { AuditFindingDto } from "../../features/auth/types/role";

interface FindingsListProps {
    findings: AuditFindingDto[];
}

const FindingsList = ({
    findings,
}: FindingsListProps) => {

    return (

        <div className="mb-4">

            <h4 className="mb-3">
                Findings
            </h4>

            {findings.map((finding, index) => (

                <div
                    key={index}
                    className="card shadow-sm mb-3"
                >

                    <div className="card-body">

                        <span className="badge bg-danger mb-2">
                            {finding.severity}
                        </span>

                        <h5>
                            {finding.title}
                        </h5>

                        <p className="mb-0">
                            {finding.description}
                        </p>

                    </div>

                </div>

            ))}

        </div>

    );

};

export default FindingsList;