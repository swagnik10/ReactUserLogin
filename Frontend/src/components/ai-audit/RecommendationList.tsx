import type { AuditRecommendationDto } from "../../features/auth/types/role";

interface RecommendationListProps {
    recommendations: AuditRecommendationDto[];
}

const RecommendationList = ({
    recommendations,
}: RecommendationListProps) => {

    return (

        <div className="mb-4">

            <h4 className="mb-3">
                Recommendations
            </h4>

            {recommendations.map((recommendation, index) => (

                <div
                    key={index}
                    className="card shadow-sm mb-3"
                >

                    <div className="card-body">

                        <span className="badge bg-primary mb-2">
                            {recommendation.priority}
                        </span>

                        <h5>
                            {recommendation.title}
                        </h5>

                        <p className="mb-0">
                            {recommendation.description}
                        </p>

                    </div>

                </div>

            ))}

        </div>

    );

};

export default RecommendationList;