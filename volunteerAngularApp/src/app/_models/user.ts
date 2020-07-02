import { Role } from './role';

export class User {
  id?: number;
  email: string;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  role: Role;
  latitude?: string;
  longitude?: string;
  token?: string;
}
