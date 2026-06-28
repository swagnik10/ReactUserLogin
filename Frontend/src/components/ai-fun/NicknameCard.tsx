import { useState } from "react";
import { toast } from "sonner";

import AiFunCard from "./AiFunCard";

import { getStoredUser } from "../../features/auth/authStorage";
import { generateNickname } from "../../services/aifun/aiFunService";

import type { NicknameResponse } from "../../features/auth/types/aiFun";

const NicknameCard = () => {
    const [loading, setLoading] = useState(false);
    const [response, setResponse] =
        useState<NicknameResponse>();

    const handleGenerate = async () => {
        try {
            const user = getStoredUser();

            if (!user) {
                toast.error("User not found.");
                return;
            }

            setLoading(true);

            const result =
                await generateNickname(user);

            setResponse(result);
        } catch (error) {
            toast.error(
                error instanceof Error
                    ? error.message
                    : "Nickname generation failed."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <AiFunCard
            title="😎 Nickname"
            description="Generate a fun developer nickname."
            loading={loading}
            onGenerate={handleGenerate}
        >
            {response && (
                <>
                    <div className="text-4xl">
                        {response.emoji}
                    </div>

                    <h3 className="mt-3 text-lg font-bold">
                        {response.nickname}
                    </h3>

                    <p className="mt-2 text-sm text-gray-600">
                        {response.reason}
                    </p>
                </>
            )}
        </AiFunCard>
    );
};

export default NicknameCard;