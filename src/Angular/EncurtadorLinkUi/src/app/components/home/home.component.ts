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
  isLoading: boolean = false;

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

    this.activatedRoute.queryParams.subscribe(val=>{
      if(val['status'] === "loggedout"){
        setTimeout(() => {
          this.toast.warning({detail:'Aviso', summary:'Você está desconectado.', duration:5000});
        }, 0); 
      }
      if(val['status'] === "loggedin"){
        setTimeout(() => {
          this.navcolor = "nav-green";
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

      this.apiService.encurtarUrl(model)
      .subscribe({
        next:(res)=>{
          this.toast.success({
            detail: 'Feito',
            summary: 'Link encurtado com sucesso.',
            duration: 5000
          });
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

  toLogin(){
    window.open("/login","_self")
  }

  logOut(){
    this.authService.signOut();
  }

  /*
  isAbsoluteUrl(url: string): boolean {
    try {
      new URL(url);  // This will throw if the URL is not valid
      return true;
    } catch {
      return false;
    }
  }
  */
}
