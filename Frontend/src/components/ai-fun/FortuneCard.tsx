import { useState } from "react";
import { toast } from "sonner";

import AiFunCard from "./AiFunCard";

import { getStoredUser } from "../../features/auth/authStorage";
import { generateFortune } from "../../services/aifun/aiFunService";

import type { FortuneResponse } from  "../../features/auth/types/aiFun";

const FortuneCard = () => {
    const [loading, setLoading] = useState(false);
    const [response, setResponse] =
        useState<FortuneResponse>();

    const handleGenerate = async () => {
        try {
            const user = getStoredUser();

            if (!user) {
                toast.error("User not found.");
                return;
            }

            setLoading(true);

            const result =
                await generateFortune(user);

            setResponse(result);
        } catch (error) {
            toast.error(
                error instanceof Error
                    ? error.message
                    : "Fortune generation failed."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <AiFunCard
            title="🥠 Developer Fortune"
            description="Reveal today's developer fortune."
            loading={loading}
            onGenerate={handleGenerate}
        >
            {response && (
                <p className="text-center text-gray-700">
                    {response.fortune}
                </p>
            )}
        </AiFunCard>
    );
};

export default FortuneCard;