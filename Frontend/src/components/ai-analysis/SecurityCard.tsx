type Props = {
    implications: string[];
};

const SecurityCard = ({ implications }: Props) => (
    <div className="rounded-lg border border-red-200 p-5">
        <h2 className="text-xl font-semibold mb-3">
            Security Implications
        </h2>

        <ul className="list-disc pl-5 space-y-2">
            {implications.map((item, index) => (
                <li key={index}>{item}</li>
            ))}
        </ul>
    </div>
);

export default SecurityCard