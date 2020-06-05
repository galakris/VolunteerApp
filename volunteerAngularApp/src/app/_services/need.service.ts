import { Need } from './../_models/need';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NeedService {

  constructor(private http: HttpClient) { }

  addNeed(need: Need){
    return this.http.post<Need>(`localhost:8080/needs`, need);
  }
}
