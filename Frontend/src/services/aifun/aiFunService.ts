import axios from "axios";

import apiClient from "../apiClient";

import type {
    AchievementResponse,
    FortuneResponse,
    NicknameResponse,
    RoastResponse,
    UserFunRequest,
} from "../../features/auth/types/aiFun";

export const generateNickname = async (
    request: UserFunRequest
): Promise<NicknameResponse> => {
    try {
        const response = await apiClient.post(
            "/ai-fun/nickname",
            request
        );

        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            throw new Error(
                error.response?.data?.Message ??
                error.response?.data?.message ??
                "Nickname generation failed."
            );
        }

        throw new Error("Nickname generation failed.");
    }
};

export const generateRoast = async (
    request: UserFunRequest
): Promise<RoastResponse> => {
    try {
        const response = await apiClient.post(
            "/ai-fun/roast",
            request
        );

        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            throw new Error(
                error.response?.data?.Message ??
                error.response?.data?.message ??
                "Roast generation failed."
            );
        }

        throw new Error("Roast generation failed.");
    }
};

export const generateFortune = async (
    request: UserFunRequest
): Promise<FortuneResponse> => {
    try {
        const response = await apiClient.post(
            "/ai-fun/fortune",
            request
        );

        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            throw new Error(
                error.response?.data?.Message ??
                error.response?.data?.message ??
                "Fortune generation failed."
            );
        }

        throw new Error("Fortune generation failed.");
    }
};

export const generateAchievement = async (
    request: UserFunRequest
): Promise<AchievementResponse> => {
    try {
        const response = await apiClient.post(
            "/ai-fun/achievement",
            request
        );

        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            throw new Error(
                error.response?.data?.Message ??
                error.response?.data?.message ??
                "Achievement generation failed."
            );
        }

        throw new Error("Achievement generation failed.");
    }
};