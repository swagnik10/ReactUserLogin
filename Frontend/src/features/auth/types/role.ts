export interface PermissionDto {
    name: string;
    category: string;
    granted: boolean;
}

export interface RoleDto {
    name: string;
    permissions: PermissionDto[];
    description: string;
}

export interface RoleSummaryDto {
    name: string;
    permissionCount: number;
    description: string;
}

export interface RiskDto {
    type: string;
    description: string;
}

export interface RecommendationDto {
    title: string;
    priority: string;
    description: string;
}

export interface RoleAnalysisDto {
    summary: string;
    capabilities: string[];
    restrictions: string[];
    risks: RiskDto[];
    riskLevel: string;
    recommendations: RecommendationDto[];
}

export interface RoleComparisonDto {
    summary: string;
    similarities: string[];
    differences: string[];
    permissionsOnlyInRoleA: string[];
    permissionsOnlyInRoleB: string[];
    recommendedUseCases: string[];
    securityImplications: string[];
}

export interface AskRbacQuestionRequest {
    question: string;
}

export interface AuditScoreDto {
    security: number;
    maintainability: number;
    leastPrivilege: number;
    consistency: number;
    overall: number;
}

export interface RoleSummaryAnalysisDto {
    name: string;
    reason: string;
}

export interface AuditFindingDto {
    severity: string;
    title: string;
    description: string;
}

export interface AuditRecommendationDto {
    priority: string;
    title: string;
    description: string;
}

export interface AskRbacQuestionResponse {
    answer: string;
    findings: AuditFindingDto[];
    recommendations: AuditRecommendationDto[];
}

export interface RbacAuditDto {
    overallRisk: string;
    score: AuditScoreDto;

    bestDesignedRole: RoleSummaryAnalysisDto;
    mostPrivilegedRole: RoleSummaryAnalysisDto;
    mostRestrictedRole: RoleSummaryAnalysisDto;

    findings: AuditFindingDto[];
    recommendations: AuditRecommendationDto[];
}