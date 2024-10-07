import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  nome:string='';
  logado: boolean = false;

  constructor(
    private authService: AuthService,
    private userStoreService: UserStoreService
  ) { }

  ngOnInit(): void {
    this.logado = this.authService.isLoggedInValid();
    this.userStoreService.getNomeFromStore()
    .subscribe(val=>{
      let nomeFromToken = this.authService.getNameFromToken();
      this.nome = val || nomeFromToken
    });
  }

  logOut(){
    this.authService.signOut();
  }
}
