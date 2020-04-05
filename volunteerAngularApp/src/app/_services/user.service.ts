import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '@/_models/user';


@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<User[]>(`localhost:8080/users`);
    }

    getById(id: number) {
        return this.http.get<User>(`localhost:8080/users/${id}`);
    }
}
