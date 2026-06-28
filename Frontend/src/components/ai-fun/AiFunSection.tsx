import NicknameCard from "./NicknameCard";
import RoastCard from "./RoastCard";
import FortuneCard from "./FortuneCard";
import AchievementCard from "./AchievementCard";

const AiFunSection = () => {
    return (
        <div className="mt-10">
            <h2 className="mb-6 text-2xl font-bold">
                ✨ AI Fun Zone
            </h2>

            <div className="grid gap-6 md:grid-cols-2">
                <NicknameCard />
                <RoastCard />
                <FortuneCard />
                <AchievementCard />
            </div>
        </div>
    );
};

export default AiFunSection;