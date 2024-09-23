import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api/api.service';
import { EncryptionService } from 'src/app/services/encryption.service';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {
  codigo!: string;
  decifrado!:string;
  valido: boolean = false;
  
  constructor(
    private apiService: ApiService,
    private activatedRoute: ActivatedRoute,
    private crypto: EncryptionService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(val=>{
      this.codigo = decodeURIComponent(val['code']);
      this.decifrado = this.crypto.decryptUsingAES256(decodeURIComponent(this.codigo));
      this.valido = this.decifrado.includes("__valido__");
    });

    this.verificarCliente();
  }

  verificarCliente(){
    if(this.valido){
      let model = {
        Codigo: this.codigo
      };
      
      this.apiService.validarCliente(model).subscribe({
        next: (res) =>{
          window.open("home/?status=confirmed","_self")
        },
        error: (err) =>{
          window.open("home/?status=unconfirmed","_self")
        }
      });
    } else {
      window.open("home/?status=unconfirmed","_self")
    }
  }
}
