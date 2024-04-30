import { ResetPasswordService } from './../../services/reset-password.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmPasswordValidator } from 'src/app/helpers/confirm-password.validator';
import ValidateForm from 'src/app/helpers/validateForm';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-reset',
  templateUrl: './reset.component.html',
  styleUrls: ['./reset.component.css']
})
export class ResetComponent implements OnInit {

  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa fa-eye-slash";

  resetPasswordForm!: FormGroup;
  emailToReset!: string;
  emailToken!: string;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private resetPasswordService: ResetPasswordService,
    private toast: NgToastService
  ) { }

  ngOnInit(): void {
    this.resetPasswordForm = this.fb.group({
      senha: [null, Validators.required],
      reSenha: [null, Validators.required]
    },{
      validator: ConfirmPasswordValidator("senha", "reSenha")
    });

    this.activatedRoute.queryParams.subscribe(val=>{
      this.emailToReset = val['email'];
      this.emailToken = decodeURIComponent(val['code']);
    })
  }

  onResetConfirm(){
    if(this.resetPasswordForm.valid){
      let model = {
        Email: this.emailToReset,
        EmailToken: this.emailToken.replace(/ /g,'+'),
        NovaSenha: this.resetPasswordForm.value.senha,
        ReNovaSenha: this.resetPasswordForm.value.reSenha
      };

      this.resetPasswordService.resetPassword(model)
      .subscribe({
        next:(res)=>{
          this.toast.success({
            detail: 'Sucesso',
            summary: 'Sua senha foi atualizada com sucesso',
            duration: 5000
          });

          this.router.navigate(['login']);
        },
        error:(err)=>{
          this.toast.error({
            detail: 'Algo deu errado',
            summary: 'Por favor tente novamente mais tarde.',
            duration: 5000
          })
        }
      })
    }else{
      ValidateForm.validateAllFormFields(this.resetPasswordForm);
    }
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.eyeIcon = this.isText ? "fa fa-eye" : "fa fa-eye-slash";
    this.type = this.isText ? "text" : "password";
  }

}
