import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { EncryptionService } from './../../services/encryption.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = environment.apiServer + '/api';
  private userPayload:any;

  constructor(
    private http: HttpClient,
    private router: Router,
    private encryptionService: EncryptionService
    ) {
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

  isLoggedInValid(): boolean{

    if(!localStorage.getItem('token'))
      return false;

    let decodedToken = this.decodedToken();

    if(!decodedToken.loginkey)
      return false;

    let loginkey = this.encryptionService.decryptUsingAES256(decodedToken.loginkey);

    return decodedToken.name === loginkey;
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token);
  }

  getNameFromToken(){
    if(this.userPayload)
      return this.userPayload.firstname + ',';

    return '';
  }

  getRoleFromToken(){
    if(this.userPayload)
      return this.userPayload.role;
  }
}
