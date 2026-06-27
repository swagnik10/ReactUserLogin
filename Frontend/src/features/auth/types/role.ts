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