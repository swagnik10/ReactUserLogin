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
        <div className="space-y-4">

            <h2 className="text-xl font-semibold text-gray-900">
                Health Scores
            </h2>

            <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-5">

                {cards.map((card) => (

                    <div
                        key={card.title}
                        className="rounded-xl border border-gray-200 bg-white p-6 text-center shadow-sm transition hover:shadow-md"
                    >
                        <p className="text-sm font-medium text-gray-500">
                            {card.title}
                        </p>

                        <div className="mt-3 text-4xl font-bold text-blue-600">
                            {card.value}
                        </div>

                        <p className="mt-1 text-sm text-gray-400">
                            /100
                        </p>
                    </div>

                ))}

            </div>

        </div>
    );
};

export default AuditScoreCards;