import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth/auth.service';
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(
    private authService: AuthService,
    private toast: NgToastService,
    private router: Router
    ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const myToken = this.authService.getToken();

    if(myToken){
      request = request.clone({
        setHeaders: {Authorization:`Bearer ${myToken}`}
      })
    }

    return next.handle(request).pipe(
      catchError((err:any)=>{
        if(err instanceof HttpErrorResponse){
          if(err.status === 401){
            return this.handleUnAuthorizedError(request, next);
          }

          if(err.status === 403){
            this.toast.warning({detail:'Aviso', summary:'Acesso não permitido.'});
            this.router.navigate(['home']);
          }
        }

        return throwError(()=> new Error('Erro interno não mapeado.'));
      })
    );
  }

  handleUnAuthorizedError(req: HttpRequest<any>, next: HttpHandler){
      return this.authService.renewToken({
        AccessToken: this.authService.getToken(),
        RefreshToken: this.authService.getRefreshToken()
      })
      .pipe(
        switchMap((data:any)=>{
          this.authService.storeRefreshToken(data.refreshToken);
          this.authService.storeToken(data.accessToken);

          req = req.clone({
            setHeaders: {Authorization:`Bearer ${data.accessToken}`}
          });

          return next.handle(req);
        }),
        catchError((err)=>{
          return throwError(()=>{
            this.toast.warning({detail:'Aviso', summary:'Você está desconectado.'});
            this.authService.signOut();
          })
        })
      )
  }

}
