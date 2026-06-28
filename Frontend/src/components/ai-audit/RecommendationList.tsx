import type { AuditRecommendationDto } from "../../features/auth/types/role";

interface RecommendationListProps {
    recommendations: AuditRecommendationDto[];
}

const RecommendationList = ({
    recommendations,
}: RecommendationListProps) => {

    const getPriorityClasses = (priority: string) => {
        switch (priority.toLowerCase()) {
            case "critical":
            case "high":
                return "bg-red-100 text-red-800";

            case "medium":
                return "bg-yellow-100 text-yellow-800";

            case "low":
                return "bg-green-100 text-green-800";

            default:
                return "bg-blue-100 text-blue-800";
        }
    };

    return (
        <div className="space-y-4">

            <h2 className="text-xl font-semibold text-gray-900">
                Recommendations
            </h2>

            {recommendations.map((recommendation, index) => (
                <div
                    key={index}
                    className="rounded-xl border border-gray-200 bg-white p-6 shadow-sm transition hover:shadow-md"
                >
                    <span
                        className={`inline-flex rounded-full px-3 py-1 text-xs font-semibold ${getPriorityClasses(
                            recommendation.priority
                        )}`}
                    >
                        {recommendation.priority}
                    </span>

                    <h3 className="mt-4 text-lg font-semibold text-gray-900">
                        {recommendation.title}
                    </h3>

                    <p className="mt-2 text-sm leading-6 text-gray-600">
                        {recommendation.description}
                    </p>
                </div>
            ))}

        </div>
    );
};

export default RecommendationList;