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
        <div className="container py-4">

            <div className="card shadow-sm">

                <div className="card-body">

                    <h2 className="card-title mb-2">
                        AI RBAC Analysis
                    </h2>

                    <p className="text-muted mb-4">
                        Use AI to ask questions about your RBAC model or run a
                        complete security audit.
                    </p>

                    <ul className="nav nav-tabs mb-4">

                        <li className="nav-item">

                            <button
                                className={`nav-link ${
                                    activeTab === "ask" ? "active" : ""
                                }`}
                                disabled={disableTabs}
                                onClick={() => setActiveTab("ask")}
                            >
                                Ask AI about RBAC
                            </button>

                        </li>

                        <li className="nav-item">

                            <button
                                className={`nav-link ${
                                    activeTab === "audit" ? "active" : ""
                                }`}
                                disabled={disableTabs}
                                onClick={() => setActiveTab("audit")}
                            >
                                RBAC Security Audit
                            </button>

                        </li>

                    </ul>

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
    );
};

export default AIAudit;