type ComparisonSummaryProps = {
    summary: string;
};

const ComparisonSummary = ({
    summary,
}: ComparisonSummaryProps) => {
    return (
        <div className="bg-white rounded-lg border shadow-sm p-6">
            <h2 className="text-xl font-semibold mb-3">
                Summary
            </h2>

            <p className="text-gray-700 leading-relaxed">
                {summary}
            </p>
        </div>
    );
};

export default ComparisonSummary;