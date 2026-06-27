import apiClient from "../apiClient";
import type {
  AgentPlan,
  ExecutePlanResponse,
} from "../../features/auth/types/agent";
import axios from "axios";

export const generatePlan = async (
  prompt: string
): Promise<AgentPlan> => {
  try{
      const response = await apiClient.post(
        "/agent/plan",
        { prompt }
      );

      return response.data;
  }
  catch (error) {
    if (axios.isAxiosError(error)) {
        throw new Error(
            error.response?.data?.Message ||
            error.response?.data?.message ||
            "AI Plan generation failed"
        );
    }

    throw new Error("AI Plan generation failed");
  }

};

export const executePlan = async (
  plan: AgentPlan
): Promise<ExecutePlanResponse> => {

  try{
      const response = await apiClient.post(
        "/agent/execute",
        { plan }
      );
    
      return response.data;
  }
  catch (error) {
    if (axios.isAxiosError(error)) {
        throw new Error(
            error.response?.data?.Message ||
            error.response?.data?.message ||
            "AI Execution failed"
        );
    }

    throw new Error("AI Execution failed");
  }
};