export interface AgentStep {
  stepNumber: number;
  action: string;
  description: string;
  parameters: Record<string, string>;
}

export interface AgentPlan {
  steps: AgentStep[];
}

export interface AgentExecutionResult {
  success: boolean;
  messages: string[];
}

export interface AgentPromptRequest {
  prompt: string;
}