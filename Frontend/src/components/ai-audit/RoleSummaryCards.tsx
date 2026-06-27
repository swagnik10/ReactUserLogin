import type { RoleSummaryAnalysisDto } from "../../features/auth/types/role";

interface RoleSummaryCardsProps {
    bestDesignedRole: RoleSummaryAnalysisDto;
    mostPrivilegedRole: RoleSummaryAnalysisDto;
    mostRestrictedRole: RoleSummaryAnalysisDto;
}

const RoleSummaryCards = ({
    bestDesignedRole,
    mostPrivilegedRole,
    mostRestrictedRole,
}: RoleSummaryCardsProps) => {

    const cards = [
        {
            title: "Best Designed Role",
            role: bestDesignedRole,
        },
        {
            title: "Most Privileged Role",
            role: mostPrivilegedRole,
        },
        {
            title: "Most Restricted Role",
            role: mostRestrictedRole,
        },
    ];

    return (

        <div className="mb-4">

            <h4 className="mb-3">
                Role Summary
            </h4>

            <div className="row g-3">

                {cards.map(card => (

                    <div
                        key={card.title}
                        className="col-md-4"
                    >

                        <div className="card h-100 shadow-sm">

                            <div className="card-body">

                                <h6 className="text-muted">
                                    {card.title}
                                </h6>

                                <h5 className="fw-bold">
                                    {card.role.name}
                                </h5>

                                <p className="mb-0">
                                    {card.role.reason}
                                </p>

                            </div>

                        </div>

                    </div>

                ))}

            </div>

        </div>

    );

};

export default RoleSummaryCards;