import apiClient from "../apiClient";
import type { AskRbacQuestionRequest, AskRbacQuestionResponse, RbacAuditDto, RoleAnalysisDto, RoleComparisonDto, RoleDto, RoleSummaryDto } from "../../features/auth/types/role";
import axios from "axios";

export const getRoles = async (): Promise<RoleSummaryDto[]> => {
    try{
        const response = await apiClient.get<RoleSummaryDto[]>("/roles");
    
        return response.data;
    }
    catch (error) {
    if (axios.isAxiosError(error)) {
        throw new Error(
            error.response?.data?.Message ||
            error.response?.data?.message ||
            "Get role summary failed"
        );
    }

    throw new Error("Get role summary failed");
  }
};

export const getRole = async (
    roleName: string
): Promise<RoleDto> => {
    try{
        const response = await apiClient.get<RoleDto>(
            `/roles/${roleName}`
        );
    
        return response.data;
    }
    catch (error) {
    if (axios.isAxiosError(error)) {
        throw new Error(
            error.response?.data?.Message ||
            error.response?.data?.message ||
            `Get role ${roleName} failed`
        );
    }

    throw new Error(`Get role ${roleName} failed`);
  }
};

export const analyzeRole = async (
    roleName: string
): Promise<RoleAnalysisDto> => {
    try{
        const response = await apiClient.get<RoleAnalysisDto>(
            `/roles/${roleName}/analyze`
        );
    
        return response.data;
    }
    catch (error) {
    if (axios.isAxiosError(error)) {
        throw new Error(
            error.response?.data?.Message ||
            error.response?.data?.message ||
            `AI Role ${roleName} Analyze failed`
        );
    }

    throw new Error(`AI role ${roleName} analyze failed`);
  }
};

export const compareRoles = async (
    roleA: string,
    roleB: string
): Promise<RoleComparisonDto> => {
    try{
        const response = await apiClient.post<RoleComparisonDto>(
            "/roles/compare",
            {
                roleA,
                roleB,
            });
    
        return response.data;
    }
    catch(error)
    {
        if (axios.isAxiosError(error)) {
        throw new Error(
            error.response?.data?.Message ||
            error.response?.data?.message ||
            `AI Role comparer between ${roleA} and ${roleB} failed`
        );
    }

    throw new Error(`AI Role comparer between ${roleA} and ${roleB} failed`);
    }
};

export const askRbacQuestion = async (
    request: AskRbacQuestionRequest
): Promise<AskRbacQuestionResponse> => {

    try{
        const response = await apiClient.post<AskRbacQuestionResponse>(
            "/roles/ask-ai",
            request
        );
    
        return response.data;
    }
    catch(error)
    {
        if (axios.isAxiosError(error)) {
        throw new Error(
            error.response?.data?.Message ||
            error.response?.data?.message ||
            `Ask Ai Feature failed`
        );
    }

    throw new Error(`Ask Ai Feature failed`);
    }
};

export const auditRbac = async (): Promise<RbacAuditDto> => {

    try{
        const response = await apiClient.post<RbacAuditDto>(
            "/roles/audit"
        );

        return response.data;
    }

    catch(error)
    {
        if (axios.isAxiosError(error)) {
        throw new Error(
            error.response?.data?.Message ||
            error.response?.data?.message ||
            `Audit Ai Feature failed`
        );
    }

    throw new Error(`Audit Ai Feature failed`);
    }


};