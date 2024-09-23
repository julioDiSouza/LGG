import { AuthService } from 'src/app/services/auth/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import ValidateForm from 'src/app/helpers/validateForm';
import { Router } from '@angular/router';
import { matchpassword } from 'src/app/helpers/matchpassword.validator.validator';
import { strongPassword } from 'src/app/helpers/strong-password.validator';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa fa-eye-slash";
  signUpForm!: FormGroup;
  isLoadingCriar = false;
  isValidEmail!: boolean;
  clicado: boolean = false;
  inputEmail!: string;
  filaEmail: boolean = false;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router, private toast: NgToastService) { }

  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      nomeCompleto: ['', Validators.required],
      email: ['', Validators.required],
      senha: [null, Validators.required],
      reSenha: [null, Validators.required]
    },
  {
    validators: [matchpassword, strongPassword]
  })
  }

  get passwordFormField() {
    return this.signUpForm.get('senha');
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.eyeIcon = this.isText ? "fa fa-eye" : "fa fa-eye-slash";
    this.type = this.isText ? "text" : "password";
  }

  onSignUp(){
    if(this.signUpForm.valid && this.checkValidEmail(this.inputEmail)){
      this.isLoadingCriar = true;
      this.auth.signup(this.signUpForm.value).subscribe({
        next: (res) => {
          this.signUpForm.reset();
          this.filaEmail = true;  
        },
        error: (err) => {
          this.toast.error({
            detail: 'Erro',
            summary: 'Falha ao realizar Login.',
            duration: 10000,
          });
          this.isLoadingCriar = false;
        },
        complete: () => {
          this.isLoadingCriar = false;
        }
      })
    }else{
      ValidateForm.validateAllFormFields(this.signUpForm);
    }
  }

  toLogin(){
    //window.open("/login","_self")
    this.router.navigate(['login']);
  }

  checkValidEmail(event: string) {
    const value = event;
    const pattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$/;
    this.isValidEmail = pattern.test(value);
    return this.isValidEmail;
  }
}
