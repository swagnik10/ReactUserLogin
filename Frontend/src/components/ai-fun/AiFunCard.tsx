import Button from "../ui/Button";

interface AiFunCardProps {
    title: string;
    description: string;
    loading: boolean;
    onGenerate: () => void;
    children?: React.ReactNode;
}

const AiFunCard = ({
    title,
    description,
    loading,
    onGenerate,
    children,
}: AiFunCardProps) => {
    return (
        <div className="rounded-xl bg-white p-6 shadow">
            <h2 className="text-xl font-semibold">
                {title}
            </h2>

            <p className="mt-2 text-sm text-gray-600">
                {description}
            </p>

            <div className="mt-4">
                <Button
                    onClick={onGenerate}
                    disabled={loading}
                >
                    {loading ? "Generating..." : "Generate"}
                </Button>
            </div>

            {children && (
                <div className="mt-6 rounded-lg border bg-gray-50 p-4">
                    {children}
                </div>
            )}
        </div>
    );
};

export default AiFunCard;