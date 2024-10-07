import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService {

  private baseUrl: string = environment.apiServer + '/api'

  constructor(
    private http: HttpClient
  ) { }

  sendResetPasswordLink(email:any){
    return this.http.post<any>(`${this.baseUrl}/usuario/atualizacao-senha`, email);
  }

  resetPassword(model:any){
    return this.http.post<any>(`${this.baseUrl}/usuario/nova-senha`, model);
  }


}
