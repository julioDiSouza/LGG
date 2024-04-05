import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  //private baseUrl: string = "https://l4u.in/api";
  private baseUrl: string = "https://localhost:7181/api";

  constructor(
    private http: HttpClient
  ) { }

  ping(){
    return this.http.get<any>(`${this.baseUrl}/teste/ping`);
  }

  autenticado(){
    return this.http.get<any>(`${this.baseUrl}/teste/autenticacao`);
  }

  autenticadoAdmin(){
    return this.http.get<any>(`${this.baseUrl}/teste/autenticacao-admin`);
  }

}
