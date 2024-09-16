import { AuthService } from 'src/app/services/auth/auth.service';
import { FormGroup, FormBuilder, Validators, FormControl, AbstractControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import ValidateForm from 'src/app/helpers/validateForm';
import { Router } from '@angular/router';
import { matchpassword } from 'src/app/helpers/matchpassword.validator.validator';
import { StrongPasswordRegx } from 'src/app/helpers/strong.password.regx';

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
  public isValidEmail!: boolean;
  clicado: boolean = false;
  public inputEmail!: string;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      nomeCompleto: ['', Validators.required],
      email: ['', Validators.required],
      senha: [null, Validators.required],
      reSenha: [null, Validators.required]
    },
  {
    validators:matchpassword
  })
  }

  get passwordFormField() {
    return this.signUpForm.get('senha');
  }

  get senhaValida() {
    return StrongPasswordRegx.test(this.passwordFormField?.value);
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.eyeIcon = this.isText ? "fa fa-eye" : "fa fa-eye-slash";
    this.type = this.isText ? "text" : "password";
  }

  onSignUp(){
    debugger
    if(this.signUpForm.valid && this.checkValidEmail(this.inputEmail) && this.senhaValida){
      this.isLoadingCriar = true;
      debugger



    }else{
      ValidateForm.validateAllFormFields(this.signUpForm);
    }
  }

  toLogin(){
    window.open("/login","_self")
  }

  checkValidEmail(event: string) {
    const value = event;
    const pattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$/;
    this.isValidEmail = pattern.test(value);
    return this.isValidEmail;
  }





}
