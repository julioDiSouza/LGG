import { UserStoreService } from './../../services/user-store.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import ValidateForm from 'src/app/helpers/validateForm';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa fa-eye-slash";
  loginForm!: FormGroup;


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
          const tokenPayLoad = this.authService.decodedToken();
          this.userStoreService.setNomeForStore(tokenPayLoad.name);
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

}
