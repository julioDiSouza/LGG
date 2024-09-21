import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserStoreService } from './../../services/user-store.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth/auth.service';
import { urlValidator } from 'src/app/helpers/url.validator';
import ValidateForm from 'src/app/helpers/validateForm';
import { ApiService } from 'src/app/services/api/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  urlForm!: FormGroup;
  logado: boolean = false;
  nome:string='';
  role:string='';
  navcolor: string='';
  linkCurto: string='';
  isLoading: boolean = false;
  encurtado: boolean = false;

  constructor(
    private authService: AuthService,
    private apiService: ApiService,
    private userStoreService: UserStoreService, 
    private toast: NgToastService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.urlForm = this.fb.group({
      url: ['', Validators.required]
    },{
      validators: urlValidator
    })

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

    if(this.logado)
      this.navcolor = "nav-color";

    this.activatedRoute.queryParams.subscribe(val=>{
      if(val['status'] === "loggedout"){
        setTimeout(() => {
          this.toast.warning({detail:'Aviso', summary:'Você está desconectado.', duration:5000});
        }, 0); 
      }
      if(val['status'] === "loggedin"){
        setTimeout(() => {
          this.toast.success({detail:'Aviso', summary:'Você está conectado.', duration:5000});
        }, 0); 
      }
    });
  }

  onEncurtar(){
    if(this.urlForm.valid){
      this.isLoading = true;
      let model = {
        UrlLonga: this.urlForm.value.url
      }

      this.apiService.encurtarUrl(model, this.authService.isLoggedIn())
      .subscribe({
        next:(res)=>{
          this.linkCurto = res.urlCurta;
          this.encurtado = true;
          this.isLoading = false;
          this.urlForm.reset();
        },
        error:(err)=>{
          this.toast.error({
            detail: 'Algo deu errado',
            summary: 'Por favor tente novamente mais tarde.',
            duration: 5000
          });
          this.isLoading = false;
        }
      });
    }else{
      ValidateForm.validateAllFormFields(this.urlForm);
    }
  }

  copiar(){
    if (navigator.clipboard) {
      navigator.clipboard.writeText(this.linkCurto)
        .then(() => {
          this.toast.success({
            detail: 'Copiado',
            summary: 'Ctrl + V disponível.',
            duration: 5000
          });
        })
        .catch(err => {
          this.toast.error({
            detail: 'Indisponível',
            summary: 'Copie manualmente.',
            duration: 5000
          });
        });
    } else {
      this.toast.error({
        detail: 'Indisponível',
        summary: 'Opção não disponivel neste navegador.',
        duration: 5000
      });
    }
  }

  toLogin(){
    window.open("/login","_self")
  }

  logOut(){
    this.authService.signOut();
  }

  novoLink(){
    this.encurtado = false;
    this.linkCurto = '';
  }
}
