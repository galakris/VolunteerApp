import { Need } from './../_models/need';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NeedService {

  constructor(private http: HttpClient) { }

  addNeed(need: Need) {
    return this.http.post<Need>(`http://volunteer-identity.azurewebsites.net/api/needs`, need);
  }

  getNeeds() {
    return this.http.get<Need[]>(`http://volunteer-identity.azurewebsites.net/api/needs`);
  }
}
