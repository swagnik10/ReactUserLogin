import type { AuditScoreDto } from "../../features/auth/types/role";

interface AuditScoreCardsProps {
    score: AuditScoreDto;
}

const AuditScoreCards = ({
    score,
}: AuditScoreCardsProps) => {

    const cards = [
        {
            title: "Security",
            value: score.security,
        },
        {
            title: "Maintainability",
            value: score.maintainability,
        },
        {
            title: "Least Privilege",
            value: score.leastPrivilege,
        },
        {
            title: "Consistency",
            value: score.consistency,
        },
        {
            title: "Overall",
            value: score.overall,
        },
    ];

    return (

        <div className="mb-4">

            <h4 className="mb-3">
                Health Scores
            </h4>

            <div className="row g-3">

                {cards.map(card => (

                    <div
                        key={card.title}
                        className="col-md col-sm-6"
                    >

                        <div className="card text-center shadow-sm h-100">

                            <div className="card-body">

                                <h6 className="text-muted">
                                    {card.title}
                                </h6>

                                <h2 className="fw-bold mb-0">
                                    {card.value}
                                </h2>

                                <small className="text-muted">
                                    /100
                                </small>

                            </div>

                        </div>

                    </div>

                ))}

            </div>

        </div>

    );
};

export default AuditScoreCards;