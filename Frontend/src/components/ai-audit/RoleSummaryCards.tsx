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
            badge: "🏆",
        },
        {
            title: "Most Privileged Role",
            role: mostPrivilegedRole,
            badge: "🔐",
        },
        {
            title: "Most Restricted Role",
            role: mostRestrictedRole,
            badge: "🛡️",
        },
    ];

    return (
        <div className="space-y-4">

            <h2 className="text-xl font-semibold text-gray-900">
                Role Summary
            </h2>

            <div className="grid grid-cols-1 gap-4 lg:grid-cols-3">

                {cards.map((card) => (

                    <div
                        key={card.title}
                        className="rounded-xl border border-gray-200 bg-white p-6 shadow-sm transition hover:shadow-md"
                    >
                        <div className="flex items-center gap-2">
                            <span className="text-2xl">
                                {card.badge}
                            </span>

                            <h3 className="text-sm font-medium text-gray-500">
                                {card.title}
                            </h3>
                        </div>

                        <h4 className="mt-4 text-xl font-bold text-gray-900">
                            {card.role.name}
                        </h4>

                        <p className="mt-2 text-sm leading-6 text-gray-600">
                            {card.role.reason}
                        </p>
                    </div>

                ))}

            </div>

        </div>
    );

};

export default RoleSummaryCards;