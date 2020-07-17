import { Role } from './role';

export class User {
  id?: number;
  email: string;
  username: string;
  phoneNumber: string;
  password: string;
  firstName: string;
  lastName: string;
  role: Role;
  latitude?: number;
  longitude?: number;
  token?: string;
}
