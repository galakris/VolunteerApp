import { Observable } from 'rxjs';
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
  getNeeds(): Observable<Need[]> {
    return this.http.get<Need[]>(environment.apiUrl + `/needs`);
  }

  getMyNeeds(): Observable<Need[]> {
    return this.http.get<Need[]>(environment.apiUrl + `/needs/my`);
  }

  assignNeed(need: Need) {
    return this.http.get(environment.apiUrl + `/needs/` + need.id + `/takeExecution`);
  }

  finishNeed(need: Need) {
    return this.http.get(environment.apiUrl + `/needs/` + need.id + `/finish`);
  }

  deleteNeed(need: Need) {
    return this.http.delete(environment.apiUrl + `/needs/` + need.id);
  }

}
