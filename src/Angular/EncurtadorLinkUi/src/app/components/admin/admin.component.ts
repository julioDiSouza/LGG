import { EncryptionService } from './../../services/encryption.service';
import { ApiService } from './../../services/api/api.service';
import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  public _urlLonga!:string;
  public _urlLongaPublica!:string;

  constructor(
    private authService: AuthService,
    private toast: NgToastService,
    private apiService: ApiService,
    private encryptionService: EncryptionService) { }

  ngOnInit(): void {
  }

  logOut(){
    this.authService.signOut();
    this.toast.warning({detail:'Aviso', summary:'Você está desconectado.', duration:5000});
  }

  pingApi(){
    this.apiService.ping().subscribe({
      next:(res)=>{
        this.toast.success({detail:'Sucesso', summary:res.detail, duration:5000});
      },
      error:(err)=>{
        this.toast.error({detail:'Falha', summary:'Falha ao consultar Api.', duration:5000});
      }
    })
  }

  autenticado(){
    this.apiService.autenticado().subscribe({
      next:(res)=>{
        this.toast.success({detail:'Sucesso', summary:`${res.status} - ${res.detail}`, duration:5000});
      }
    })
  }

  autenticadoAdmin(){
    this.apiService.autenticadoAdmin().subscribe({
      next:(res)=>{
        this.toast.success({detail:'Sucesso', summary:`${res.status} - ${res.detail}`, duration:5000});
      }
    })
  }

  encrypt(){
    alert(this.encryptionService);
  }

  encurtar(){
    this.apiService.encurtarUrl({ urlLonga: this._urlLonga }).subscribe({
      next:(res)=>{
        alert(res.urlCurta);
      }
    })
  }

  encurtarPublico(){
    this.apiService.encurtarUrlPublico({ urlLonga: this._urlLongaPublica }).subscribe({
      next:(res)=>{
        alert(res.urlCurta);
      }
    })
  }

}
