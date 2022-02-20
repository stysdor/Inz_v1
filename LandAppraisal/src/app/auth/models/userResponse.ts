import { User } from "./user";

export type LoginResponse = Readonly<{
  user: User;
  //token: string;
}>;
