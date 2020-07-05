import { Need } from './../_models/need';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NeedService {

  constructor(private http: HttpClient) { }

  addNeed(need: Need) {
    return this.http.post<Need>(environment.apiUrl + `/needs`, need);
  }

  // parameters:
  // user_id  - to calc distance
  // categoty
  getNeeds() {
    return this.http.get<Need[]>(environment.apiUrl + `/needs`);
  }
}
