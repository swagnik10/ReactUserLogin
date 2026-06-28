export interface UserFunRequest {
  userId: number;
  username: string;
  firstName: string;
  lastName: string;
  role: string;
}

export interface NicknameResponse {
  nickname: string;
  emoji: string;
  reason: string;
}

export interface RoastResponse {
  roast: string;
}

export interface FortuneResponse {
  fortune: string;
}

export interface AchievementResponse {
  title: string;
  emoji: string;
  description: string;
}