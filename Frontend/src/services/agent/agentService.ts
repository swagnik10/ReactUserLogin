import apiClient from "../apiClient";
import type {
  AgentExecutionResult,
  AgentPlan,
} from "../../types/agent";

export const generatePlan = async (
  prompt: string
): Promise<AgentPlan> => {
  const response = await apiClient.post(
    "/agent/plan",
    { prompt }
  );

  return response.data;
};

export const executePlan = async (
  plan: AgentPlan
): Promise<AgentExecutionResult> => {
  const response = await apiClient.post(
    "/agent/execute",
    { plan }
  );

  return response.data;
};