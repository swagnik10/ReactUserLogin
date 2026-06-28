import { useState } from "react";

import type {
    AskRbacQuestionResponse,
    RbacAuditDto,
} from "../../features/auth/types/role";

import AskAiTab from "../../components/ai-audit/AskAiTab";
import AuditTab from "../../components/ai-audit/AuditTab";

const AIAudit = () => {
    const [activeTab, setActiveTab] = useState<"ask" | "audit">("ask");

    const [loadingAsk, setLoadingAsk] = useState(false);
    const [loadingAudit, setLoadingAudit] = useState(false);

    const [askResponse, setAskResponse] =
        useState<AskRbacQuestionResponse | null>(null);

    const [auditResponse, setAuditResponse] =
        useState<RbacAuditDto | null>(null);

    const disableTabs = loadingAsk || loadingAudit;

    return (
        <div className="mx-auto max-w-7xl p-6">
            <div className="rounded-xl border border-gray-200 bg-white shadow-sm">
                <div className="p-6">
                    <h1 className="text-2xl font-bold text-gray-900">
                        AI RBAC Analysis
                    </h1>

                    <p className="mt-2 text-gray-600">
                        Use AI to ask questions about your RBAC model or run a
                        complete security audit.
                    </p>

                    {/* Tabs */}
                    <div className="mt-6 border-b border-gray-200">
                        <nav className="flex gap-8">
                            <button
                                type="button"
                                disabled={disableTabs}
                                onClick={() => setActiveTab("ask")}
                                className={`border-b-2 px-1 py-3 text-sm font-medium transition-colors ${
                                    activeTab === "ask"
                                        ? "border-blue-600 text-blue-600"
                                        : "border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700"
                                } ${
                                    disableTabs
                                        ? "cursor-not-allowed opacity-50"
                                        : "cursor-pointer"
                                }`}
                            >
                                Ask AI about RBAC
                            </button>

                            <button
                                type="button"
                                disabled={disableTabs}
                                onClick={() => setActiveTab("audit")}
                                className={`border-b-2 px-1 py-3 text-sm font-medium transition-colors ${
                                    activeTab === "audit"
                                        ? "border-blue-600 text-blue-600"
                                        : "border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700"
                                } ${
                                    disableTabs
                                        ? "cursor-not-allowed opacity-50"
                                        : "cursor-pointer"
                                }`}
                            >
                                RBAC Security Audit
                            </button>
                        </nav>
                    </div>

                    {/* Tab Content */}
                    <div className="mt-6">
                        {activeTab === "ask" && (
                            <AskAiTab
                                loading={loadingAsk}
                                response={askResponse}
                                onLoadingChange={setLoadingAsk}
                                onResponse={setAskResponse}
                            />
                        )}

                        {activeTab === "audit" && (
                            <AuditTab
                                loading={loadingAudit}
                                response={auditResponse}
                                onLoadingChange={setLoadingAudit}
                                onResponse={setAuditResponse}
                            />
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AIAudit;