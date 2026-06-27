import { useState } from "react";

import {
    askRbacQuestion,
} from "../../services/role/roleService";

import type {
    AskRbacQuestionResponse,
} from "../../features/auth/types/role";

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
        }
        finally {
            onLoadingChange(false);
        }
    };

    return (
        <>

            <h4 className="mb-3">
                Ask AI about your RBAC Model
            </h4>

            <p className="text-muted">
                Ask questions about security,
                permissions, role hierarchy,
                least privilege and authorization.
            </p>

            <div className="d-flex flex-wrap gap-2 mb-3">

                {examples.map(example => (

                    <button
                        key={example}
                        className="btn btn-outline-secondary btn-sm"
                        disabled={loading}
                        onClick={() => setQuestion(example)}
                    >
                        {example}
                    </button>

                ))}

            </div>

            <div className="mb-3">

                <textarea
                    className="form-control"
                    rows={4}
                    disabled={loading}
                    value={question}
                    placeholder="Ask a question about your RBAC model..."
                    onChange={(e) =>
                        setQuestion(e.target.value)
                    }
                />

            </div>

            <button
                className="btn btn-primary mb-4"
                disabled={loading || !question.trim()}
                onClick={handleAsk}
            >
                {loading
                    ? "Thinking..."
                    : "Ask AI"}
            </button>

            {response && (

                <>

                    <div className="card mb-4">

                        <div className="card-header">
                            Answer
                        </div>

                        <div className="card-body">

                            {response.answer}

                        </div>

                    </div>

                </>

            )}

        </>
    );
};

export default AskAiTab;