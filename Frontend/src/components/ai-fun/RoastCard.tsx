import { useState } from "react";
import { toast } from "sonner";

import AiFunCard from "./AiFunCard";

import { getStoredUser } from "../../features/auth/authStorage";
import { generateRoast } from "../../services/aifun/aiFunService";

import type { RoastResponse } from  "../../features/auth/types/aiFun";

const RoastCard = () => {
    const [loading, setLoading] = useState(false);
    const [response, setResponse] =
        useState<RoastResponse>();

    const handleGenerate = async () => {
        try {
            const user = getStoredUser();

            if (!user) {
                toast.error("User not found.");
                return;
            }

            setLoading(true);

            const result =
                await generateRoast(user);

            setResponse(result);
        } catch (error) {
            toast.error(
                error instanceof Error
                    ? error.message
                    : "Roast generation failed."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <AiFunCard
            title="😂 Roast Me"
            description="Get a friendly AI roast."
            loading={loading}
            onGenerate={handleGenerate}
        >
            {response && (
                <p className="text-center text-gray-700">
                    {response.roast}
                </p>
            )}
        </AiFunCard>
    );
};

export default RoastCard;