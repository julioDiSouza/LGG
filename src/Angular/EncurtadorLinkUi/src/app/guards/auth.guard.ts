import { AuthService } from 'src/app/services/auth/auth.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { NgToastService } from 'ng-angular-popup';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router,
    private toast: NgToastService)
  { }

  canActivate():boolean{

    if(this.authService.isLoggedInValid()){
      return true;
    }else{
      this.toast.warning({detail:"Aviso", summary:"Você está desconectado.", duration:8000});
      this.router.navigate(['login']);
      return false;
    }
  }

}
