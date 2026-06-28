type ComparisonSectionProps = {
    title: string;
    items: string[];
};

const ComparisonSection = ({
    title,
    items,
}: ComparisonSectionProps) => {
    return (
        <div className="bg-white rounded-lg border shadow-sm p-6">

            <h2 className="text-xl font-semibold mb-4">
                {title}
            </h2>

            {items.length === 0 ? (
                <p className="text-gray-500 italic">
                    None
                </p>
            ) : (
                <ul className="space-y-3">
                    {items.map((item, index) => (
                        <li
                            key={index}
                            className="flex items-start gap-2"
                        >
                            <span className="mt-1 text-blue-600">
                                •
                            </span>

                            <span className="text-gray-700">
                                {item}
                            </span>
                        </li>
                    ))}
                </ul>
            )}

        </div>
    );
};

export default ComparisonSection;