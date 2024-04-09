import { UserStoreService } from './../../services/user-store.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import ValidateForm from 'src/app/helpers/validateForm';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm!: FormGroup;
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa fa-eye-slash";
  public resetPasswordEmail!:string;
  public isValidEmail!:boolean;
  clicado:boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toast: NgToastService,
    private userStoreService: UserStoreService
    ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['',Validators.required],
      senha: ['',Validators.required]
    })
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.eyeIcon = this.isText ? "fa fa-eye" : "fa fa-eye-slash";
    this.type = this.isText ? "text" : "password";
  }

  onLogin(){
    if(this.loginForm.valid){
      this.authService.login(this.loginForm.value)
      .subscribe({
        next:(res)=>{
          this.loginForm.reset();
          this.authService.storeToken(res.accessToken);
          this.authService.storeRefreshToken(res.refreshToken);
          const tokenPayLoad = this.authService.decodedToken();
          this.userStoreService.setNomeForStore(tokenPayLoad.firstname);
          this.userStoreService.setRoleForStore(tokenPayLoad.role);
          this.toast.success({detail:'Sucesso', summary:'Você está conectado.', duration:5000});
          this.router.navigate(['admin']);
        },
        error:(err)=>{
          this.toast.error({detail:'Erro', summary:'Falha ao realizar Login.', duration:5000});
        }
      })
    }else{
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }

  checkValidEmail(event:string){
    this.clicado = true;
    const value = event;
    const pattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$/;
    this.isValidEmail = pattern.test(value);
    return this.isValidEmail;
  }

  confirmToSend(){
    this.clicado = true;

    if(this.checkValidEmail(this.resetPasswordEmail)){

      const buttonRef = document.getElementById('closeBtn');
      buttonRef?.click();
      this.toast.success({detail:'Enviado', summary:'Verifique sua caixa de e-mail.', duration:5000});
      this.resetPasswordEmail='';
      this.clicado = false;
    }
  }

}
