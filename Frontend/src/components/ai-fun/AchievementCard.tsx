import { useState } from "react";
import { toast } from "sonner";

import AiFunCard from "./AiFunCard";

import { getStoredUser } from "../../features/auth/authStorage";
import { generateAchievement } from "../../services/aifun/aiFunService";

import type { AchievementResponse } from  "../../features/auth/types/aiFun";

const AchievementCard = () => {
    const [loading, setLoading] = useState(false);
    const [response, setResponse] =
        useState<AchievementResponse>();

    const handleGenerate = async () => {
        try {
            const user = getStoredUser();

            if (!user) {
                toast.error("User not found.");
                return;
            }

            setLoading(true);

            const result =
                await generateAchievement(user);

            setResponse(result);
        } catch (error) {
            toast.error(
                error instanceof Error
                    ? error.message
                    : "Achievement generation failed."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <AiFunCard
            title="🏆 Achievement"
            description="Unlock a fun AI-generated achievement."
            loading={loading}
            onGenerate={handleGenerate}
        >
            {response && (
                <div className="text-center">
                    <div className="text-5xl">
                        {response.emoji}
                    </div>

                    <h3 className="mt-3 text-lg font-bold">
                        {response.title}
                    </h3>

                    <p className="mt-2 text-gray-600">
                        {response.description}
                    </p>
                </div>
            )}
        </AiFunCard>
    );
};

export default AchievementCard;