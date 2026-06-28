import { useState } from "react";

import { askRbacQuestion } from "../../services/role/roleService";

import type {
    AskRbacQuestionResponse,
} from "../../features/auth/types/role";
import FindingsList from "./FindingsList";
import RecommendationList from "./RecommendationList";

interface AskAiTabProps {
    loading: boolean;
    response: AskRbacQuestionResponse | null;
    onLoadingChange: (loading: boolean) => void;
    onResponse: (
        response: AskRbacQuestionResponse | null
    ) => void;
}

const examples = [
    "Is my RBAC model secure?",
    "Which roles should be merged?",
    "Are there overlapping roles?",
    "Which role follows least privilege?",
    "Which permissions are dangerous?",
];

const AskAiTab = ({
    loading,
    response,
    onLoadingChange,
    onResponse,
}: AskAiTabProps) => {
    const [question, setQuestion] = useState("");

    const handleAsk = async () => {
        if (!question.trim()) return;

        try {
            onLoadingChange(true);

            onResponse(null);

            const result = await askRbacQuestion({ question });

            onResponse(result);
        } finally {
            onLoadingChange(false);
        }
    };

    return (
        <div className="space-y-6">
            <div>
                <h2 className="text-xl font-semibold text-gray-900">
                    Ask AI about your RBAC Model
                </h2>

                <p className="mt-2 text-sm text-gray-600">
                    Ask questions about security, permissions, role hierarchy,
                    least privilege, and authorization.
                </p>
            </div>

            <div className="flex flex-wrap gap-2">
                {examples.map((example) => (
                    <button
                        key={example}
                        type="button"
                        disabled={loading}
                        onClick={() => setQuestion(example)}
                        className="rounded-md border border-gray-300 bg-white px-3 py-2 text-sm text-gray-700 transition hover:border-blue-500 hover:text-blue-600 disabled:cursor-not-allowed disabled:opacity-50"
                    >
                        {example}
                    </button>
                ))}
            </div>

            <div>
                <textarea
                    rows={5}
                    disabled={loading}
                    value={question}
                    placeholder="Ask a question about your RBAC model..."
                    onChange={(e) => setQuestion(e.target.value)}
                    className="w-full rounded-lg border border-gray-300 px-4 py-3 text-sm shadow-sm transition focus:border-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:bg-gray-100 disabled:cursor-not-allowed"
                />
            </div>

            <div>
                <button
                    type="button"
                    disabled={loading || !question.trim()}
                    onClick={handleAsk}
                    className="rounded-lg bg-blue-600 px-5 py-2.5 text-sm font-medium text-white shadow transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400"
                >
                    {loading ? "Thinking..." : "Ask AI"}
                </button>
            </div>

            {response && (

                <div className="space-y-6">
                <div className="rounded-lg border border-gray-200 bg-white shadow-sm">
                    <div className="border-b border-gray-200 bg-gray-50 px-4 py-3">
                        <h3 className="font-semibold text-gray-900">
                            AI Answer
                        </h3>
                    </div>

                    <div className="px-4 py-4">
                        <p className="whitespace-pre-wrap text-sm leading-7 text-gray-700">
                            {response.answer}
                        </p>
                    </div>

                    

                </div>

                {response.findings.length > 0 && (
                        <FindingsList findings={response.findings} />
                    )}

                    {response.recommendations.length > 0 && (
                        <RecommendationList
                            recommendations={response.recommendations}
                        />
                    )}
                    </div>


            )}
        </div>
    );
};

export default AskAiTab;