import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '@/_models/user';


@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<User[]>(environment.apiUrl + `/users`);
  }

  getById(id: number) {
    return this.http.get<User>(environment.apiUrl + `/users/${id}`);
  }

  addUser(user: User) {
    return this.http.post<User>(environment.apiUrl + `/account/register`, user);
  }
}
