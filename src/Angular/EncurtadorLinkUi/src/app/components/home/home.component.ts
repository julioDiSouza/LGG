import { UserStoreService } from './../../services/user-store.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  logado: boolean = false;
  nome:string='';
  role:string='';

  constructor(
    private authService: AuthService,
    private userStoreService: UserStoreService, 
    private toast: NgToastService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.userStoreService.getNomeFromStore()
    .subscribe(val=>{
      let nomeFromToken = this.authService.getNameFromToken();
      this.nome = val || nomeFromToken
    });

    this.userStoreService.getRoleFromStore()
    .subscribe(val=>{
      let roleFromStore = this.authService.getRoleFromToken();
      this.role = val || roleFromStore;
    });

    this.logado = this.authService.isLoggedInValid();

    this.activatedRoute.queryParams.subscribe(val=>{
      if(val['status'] === "logout"){
        setTimeout(() => {
          this.toast.warning({detail:'Aviso', summary:'Você está desconectado.', duration:5000});
        }, 0); 
      }
    });
  }

  toLogin(){
    window.open("/login","_self")
  }

  logOut(){
    this.authService.signOut();
  }

}
