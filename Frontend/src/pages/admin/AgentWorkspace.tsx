import { useState } from "react";
import {
    generatePlan,
    executePlan,
} from "../../services/agent/agentService";
import type {
    AgentPlan,
    AgentExecutionResult,
} from "../../types/agent";
import { Eye, EyeOff } from "lucide-react";

const AgentWorkspace = () => {
    const [prompt, setPrompt] = useState("");
    const [plan, setPlan] = useState<AgentPlan | null>(null);
    const [result, setResult] =
        useState<AgentExecutionResult | null>(null);

    const [loading, setLoading] = useState(false);
    const [showPasswords, setShowPasswords] = useState<
        Record<string, boolean>
    >({});

    const handleGeneratePlan = async () => {
        try {
            setLoading(true);

            const generatedPlan =
                await generatePlan(prompt);

            generatedPlan.steps.forEach((step) => {
                if (step.action === "CreateUser") {
                    step.parameters.Password =
                        generatePassword();
                }
            });

            setPlan(generatedPlan);
            setResult(null);
        } finally {
            setLoading(false);
        }
    };

    const handleExecute = async () => {
        if (!plan) return;

        try {
            setLoading(true);

            const executionResult =
                await executePlan(plan);

            setResult(executionResult);
        } finally {
            setLoading(false);
        }
    };

    const updateParameter = (
        stepIndex: number,
        key: string,
        value: string
    ) => {
        if (!plan) return;

        const updatedPlan = structuredClone(plan);

        updatedPlan.steps[stepIndex].parameters[key] = value;

        setPlan(updatedPlan);
    };

    const canExecute = () => {
        if (!plan) return false;

        for (const step of plan.steps) {
            if (step.action === "CreateUser") {
                const requiredFields = [
                    "FirstName",
                    "LastName",
                    "PhoneNumber",
                    "Email",
                    "Username",
                    "Password",
                    
                ];

                for (const field of requiredFields) {
                    const value = step.parameters[field];

                    if (!value?.trim()) {
                        return false;
                    }
                }
            }
        }

        return true;
    };

    const generatePassword = () => {
        const random =
            Math.random().toString(36).slice(-8);

        return `Temp@${random}1`;
    };

    const generateEmail = (
        firstName?: string,
        lastName?: string
    ) => {
        const first =
            firstName?.trim().toLowerCase() || "user";

        const last =
            lastName?.trim().toLowerCase() || "";

        const random = Math.floor(
            100 + Math.random() * 900
        );

        return `${first}.${last}${random}@test.com`;
    };

    const togglePasswordVisibility = (
        stepIndex: number,
        key: string
    ) => {
        const fieldId = `${stepIndex}-${key}`;

        setShowPasswords((prev) => ({
            ...prev,
            [fieldId]: !prev[fieldId],
        }));
    };

    return (
        <div className="space-y-6">
            <h1 className="text-2xl font-bold">
                AI Workspace Agent
            </h1>

            <textarea
                className="w-full border rounded p-3"
                rows={4}
                value={prompt}
                onChange={(e) =>
                    setPrompt(e.target.value)
                }
                placeholder="Create Rahul as Admin"
            />

            <button
                onClick={handleGeneratePlan}
                disabled={loading}
                className="px-4 py-2 bg-blue-600 text-white rounded"
            >
                Generate Plan
            </button>

            {plan && (
                <div className="border rounded p-4">
                    <h2 className="font-semibold mb-4">
                        Plan Generated
                    </h2>

                    {plan.steps.map((step, stepIndex) => (
                        <div
                            key={step.stepNumber}
                            className="mb-6 rounded border p-4"
                        >
                            <div className="font-semibold">
                                Step {step.stepNumber}
                            </div>

                            <div className="mb-4 text-gray-600">
                                {step.description}
                            </div>

                            {Object.entries(step.parameters).map(
                                ([key, value]) => {
                                    const isPassword =
                                        key.toLowerCase() === "password";

                                    const isEmail =
                                        key.toLowerCase() === "email";

                                    return (
                                        <div
                                            key={key}
                                            className="mb-3"
                                        >
                                            <label className="block text-sm font-medium mb-1">
                                                {key}
                                            </label>

                                            <div className="flex gap-2">
                                                <input
                                                    type={
                                                        isPassword
                                                            ? showPasswords[
                                                                `${stepIndex}-${key}`
                                                            ]
                                                                ? "text"
                                                                : "password"
                                                            : "text"
                                                    }
                                                    value={value}
                                                    onChange={(e) =>
                                                        updateParameter(
                                                            stepIndex,
                                                            key,
                                                            e.target.value
                                                        )
                                                    }
                                                    className="flex-1 border rounded p-2"
                                                />

                                                {isPassword && (
                                                    <>
                                                        <button
                                                            type="button"
                                                            onClick={() =>
                                                                togglePasswordVisibility(
                                                                    stepIndex,
                                                                    key
                                                                )
                                                            }
                                                            className="px-3 border rounded"
                                                        >
                                                            {showPasswords[
                                                                `${stepIndex}-${key}`
                                                            ] ? (
                                                                <EyeOff size={18} />
                                                            ) : (
                                                                <Eye size={18} />
                                                            )}
                                                        </button>

                                                        <button
                                                            type="button"
                                                            onClick={() =>
                                                                updateParameter(
                                                                    stepIndex,
                                                                    key,
                                                                    generatePassword()
                                                                )
                                                            }
                                                            className="px-3 py-2 bg-gray-200 rounded hover:bg-gray-300"
                                                        >
                                                            Generate
                                                        </button>
                                                    </>
                                                )}

                                                {isEmail && (
                                                    <button
                                                        type="button"
                                                        onClick={() => {
                                                            updateParameter(
                                                                stepIndex,
                                                                key,
                                                                generateEmail(
                                                                    step.parameters
                                                                        .FirstName,
                                                                    step.parameters
                                                                        .LastName
                                                                )
                                                            );
                                                        }}
                                                        className="px-3 py-2 bg-gray-200 rounded hover:bg-gray-300"
                                                    >
                                                        Generate
                                                    </button>
                                                )}
                                            </div>
                                        </div>
                                    );
                                }
                            )}
                        </div>
                    ))}

                    {!canExecute() && (
                        <div className="mb-4 text-sm text-red-600">
                            Complete all required user fields
                            before executing the plan.
                        </div>
                    )}

                    <button
                        onClick={handleExecute}
                        disabled={loading || !canExecute()}
                        className="mt-4 px-4 py-2 bg-green-600 text-white rounded"
                    >
                        Execute Plan
                    </button>
                </div>
            )}

            {result && (
                <div className="rounded border p-4">
                    <div className="font-semibold mb-2">
                        {result.success
                            ? "Plan Executed Successfully"
                            : "Execution Failed"}
                    </div>

                    <ul className="list-disc ml-6">
                        {result.messages.map((message) => (
                            <li key={message}>
                                {message}
                            </li>
                        ))}
                    </ul>
                </div>
            )}
        </div>
    );
};

export default AgentWorkspace;