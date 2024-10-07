import { Injectable } from '@angular/core';

declare const FB: any;

@Injectable({
  providedIn: 'root'
})
export class FacebookService {

  constructor() {
    FB.init({
      appId: '1136092060971989',
      cookie: true,
      xfbml: true,
      version: 'v12.0'
    });
   }

   login(): Promise<any> {
    return new Promise((resolve, reject) => {
      FB.login((response: any) => {
        if (response.authResponse) {
          resolve(response.authResponse);
        } else {
          reject('User cancelled login or did not fully authorize.');
        }
      }, { scope: 'email' });
    });
  }

  getUserProfile(): Promise<any> {
    return new Promise((resolve, reject) => {
      FB.api('/me', { fields: 'id,name,email' }, (response: any) => {
        if (response && !response.error) {
          resolve(response);
        } else {
          reject('Error fetching user profile.');
        }
      });
    });
  }

  logout(): Promise<any> {
    return new Promise((resolve, reject) => {
      FB.logout((response: any) => {
        resolve(response);
      });
    });
  }

}
