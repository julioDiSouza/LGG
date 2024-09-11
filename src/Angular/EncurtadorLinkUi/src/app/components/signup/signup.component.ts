import { AuthService } from 'src/app/services/auth/auth.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import ValidateForm from 'src/app/helpers/validateForm';
import { Router } from '@angular/router';

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

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      nomeCompleto: ['', Validators.required],
      email: ['', Validators.required],
      senha: ['', Validators.required],
      reSenha: ['', Validators.required]
    })
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.eyeIcon = this.isText ? "fa fa-eye" : "fa fa-eye-slash";
    this.type = this.isText ? "text" : "password";
  }

  onSignUp(){
    if(this.signUpForm.valid){

      this.isLoadingCriar = true;

    }else{
      ValidateForm.validateAllFormFields(this.signUpForm);
    }
  }

  toLogin(){
    window.open("/login","_self")
  }

  checkValidEmail(event: string) {
    this.clicado = true;
    const value = event;
    const pattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$/;
    this.isValidEmail = pattern.test(value);
    return this.isValidEmail;
  }

}
