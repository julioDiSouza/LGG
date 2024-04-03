import { UserStoreService } from './../../services/user-store.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public nome:string='';
  public role:string='';

  constructor(
    private authService: AuthService,
    private userStoreService: UserStoreService
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

  }

  logOut(){
    this.authService.signOut();
  }

}
