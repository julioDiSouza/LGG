import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  //private baseUrl: string = "https://l4u.in/api";
  private baseUrl: string = "https://localhost:7181/api";

  private userPayload:any;

  constructor(
    private http: HttpClient,
    private router: Router) {
      this.userPayload = this.decodedToken();
     }

  signup(signupObj: any){
    return this.http.post<any>(`${this.baseUrl}/cliente`, signupObj);
  }

  signOut(){
    localStorage.clear();
    //localStorage.removeItem('token');
    this.router.navigate(['login']);
  }

  login(loginObj: any){
    return this.http.post<any>(`${this.baseUrl}/login`, loginObj);
  }

  renewToken(tokenDto:any){
    return this.http.post<any>(`${this.baseUrl}/token/refresh`, tokenDto);
  }

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue)
  }

  storeRefreshToken(tokenValue: string){
    localStorage.setItem('refreshToken', tokenValue)
  }

  getToken(){
    return localStorage.getItem('token');
  }

  getRefreshToken(){
    return localStorage.getItem('refreshToken');
  }

  isLoggedIn(): boolean{
    return !!localStorage.getItem('token');
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token);
  }

  getNameFromToken(){
    if(this.userPayload)
      return this.userPayload.name;
  }

  getRoleFromToken(){
    if(this.userPayload)
      return this.userPayload.role;
  }
}
