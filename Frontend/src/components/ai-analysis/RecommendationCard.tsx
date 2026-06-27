type Props = {
    recommendations: string[];
};

const RecommendationCard = ({ recommendations }: Props) => (
    <div className="rounded-lg border p-5">
        <h2 className="text-xl font-semibold mb-3">
            Recommended Usage
        </h2>

        <ul className="list-disc pl-5 space-y-2">
            {recommendations.map((item, index) => (
                <li key={index}>{item}</li>
            ))}
        </ul>
    </div>
);

export default RecommendationCard