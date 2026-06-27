import type { RoleAnalysisDto } from "../../features/auth/types/role";

interface Props {
    analysis: RoleAnalysisDto | null;
    loading: boolean;
    onAnalyze(): void;
}

function getRiskBadgeClass(level: string) {

    switch (level.toLowerCase()) {
        case "critical":
            return "bg-red-100 text-red-700";

        case "high":
            return "bg-orange-100 text-orange-700";

        case "medium":
            return "bg-yellow-100 text-yellow-700";

        case "low":
            return "bg-green-100 text-green-700";

        default:
            return "bg-gray-100 text-gray-700";
    }
}

export default function AiRoleAnalysis({
    analysis,
    loading,
    onAnalyze,
}: Props) {
    return (
        <div className="mt-6 rounded-xl border bg-white p-5 shadow-sm">
            <div className="flex items-center justify-between border-b pb-4">
                <div>
                    <h2 className="text-xl font-semibold">
                        AI Role Analysis
                    </h2>

                    <p className="mt-1 text-sm text-gray-500">
                        Analyze this role using AI-powered RBAC security review.
                    </p>
                </div>

                <button
                    onClick={onAnalyze}
                    disabled={loading}
                    className="rounded-lg bg-blue-600 px-4 py-2 text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400"
                >
                    {loading ? "Analyzing..." : "Analyze with AI"}
                </button>
            </div>

            {!analysis && !loading && (
                <div className="py-10 text-center text-gray-500">
                    Click <strong>Analyze with AI</strong> to generate a security
                    assessment for this role.
                </div>
            )}

            {loading && (
                <div className="py-10 text-center text-gray-500">
                    AI is analyzing the selected role...
                </div>
            )}

            {analysis && (
                <div className="mt-6 space-y-8">

                    {/* Summary */}
                    <section>
                        <h3 className="mb-2 text-lg font-semibold">
                            Summary
                        </h3>

                        <p className="leading-7 text-gray-700">
                            {analysis.summary}
                        </p>
                    </section>

                    {/* Risk Level */}
                    <section>
                        <h3 className="mb-2 text-lg font-semibold">
                            Risk Level
                        </h3>

                        <span className={`inline-flex rounded-full bg-gray-100 px-4 py-2 font-medium ${getRiskBadgeClass(analysis.riskLevel)}`}>
                            {analysis.riskLevel}
                        </span>
                    </section>

                    {/* Capabilities */}
                    <section>
                        <h3 className="mb-2 text-lg font-semibold">
                            Capabilities
                        </h3>

                        <ul className="list-inside list-disc space-y-2">
                            {analysis.capabilities.map(capability => (
                                <li key={capability}>
                                    {capability}
                                </li>
                            ))}
                        </ul>
                    </section>

                    {/* Restrictions */}
                    <section>
                        <h3 className="mb-2 text-lg font-semibold">
                            Restrictions
                        </h3>

                        <ul className="list-inside list-disc space-y-2">
                            {analysis.restrictions.map(restriction => (
                                <li key={restriction}>
                                    {restriction}
                                </li>
                            ))}
                        </ul>
                    </section>

                    {/* Risks */}
                    <section>
                        <h3 className="mb-4 text-lg font-semibold">
                            Risks
                        </h3>

                        <div className="space-y-4">
                            {analysis.risks.map(risk => (
                                <div
                                    key={risk.type}
                                    className="rounded-lg border p-4"
                                >
                                    <h4 className="font-semibold">
                                        {risk.type}
                                    </h4>

                                    <p className="mt-2 text-sm leading-6 text-gray-600">
                                        {risk.description}
                                    </p>
                                </div>
                            ))}
                        </div>
                    </section>

                    {/* Recommendations */}
                    <section>
                        <h3 className="mb-4 text-lg font-semibold">
                            Recommendations
                        </h3>

                        <div className="space-y-4">
                            {analysis.recommendations.map(recommendation => (
                                <div
                                    key={recommendation.title}
                                    className="rounded-lg border p-4"
                                >
                                    <div className="flex items-center justify-between">
                                        <h4 className="font-semibold">
                                            {recommendation.title}
                                        </h4>

                                        <span className={`rounded bg-gray-100 px-2 py-1 text-xs font-medium ${getRiskBadgeClass(recommendation.priority)}`}>
                                            {recommendation.priority}
                                        </span>
                                    </div>

                                    <p className="mt-2 text-sm leading-6 text-gray-600">
                                        {recommendation.description}
                                    </p>
                                </div>
                            ))}
                        </div>
                    </section>

                </div>
            )}
        </div>
    );
}