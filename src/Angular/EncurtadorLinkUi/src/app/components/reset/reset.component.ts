import { ResetPasswordService } from './../../services/reset-password.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmPasswordValidator } from 'src/app/helpers/confirm-password.validator';
import ValidateForm from 'src/app/helpers/validateForm';
import { NgToastService } from 'ng-angular-popup';
import { EncryptionService } from 'src/app/services/encryption.service';
import { matchpassword } from 'src/app/helpers/matchpassword.validator.validator';
import { strongPassword } from 'src/app/helpers/strong-password.validator';

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
  codigo!: string;
  valido: boolean = false;
  isLoading: boolean = false;
  feito: boolean = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private resetPasswordService: ResetPasswordService,
    private toast: NgToastService, 
    private crypto: EncryptionService
  ) { }

  ngOnInit(): void {
    this.resetPasswordForm = this.fb.group({
      senha: [null, Validators.required],
      reSenha: [null, Validators.required]
    },{
      /*validator: ConfirmPasswordValidator("senha", "reSenha")*/
      validators:[matchpassword, strongPassword]
    });

    this.activatedRoute.queryParams.subscribe(val=>{
      this.codigo = decodeURIComponent(val['code']);
      this.valido = this.crypto.decryptUsingAES256(decodeURIComponent(val['code'])).includes("__valido__");
    })
  }

  get passwordFormField() {
    return this.resetPasswordForm.get('senha');
  }

  onResetConfirm(){
    if(this.resetPasswordForm.valid){
      this.isLoading = true;
      let model = {
        Codigo: this.codigo,
        NovaSenha: this.resetPasswordForm.value.senha,
        ReNovaSenha: this.resetPasswordForm.value.reSenha
      };

      this.resetPasswordService.resetPassword(model)
      .subscribe({
        next:(res)=>{
          if(res.status == 200){
            this.toast.success({
              detail: 'Sucesso',
              summary: 'Sua senha foi atualizada.',
              duration: 8000
            });
          }

          if(res.status == 400){
            this.toast.warning({
              detail: 'Expirou',
              summary: 'Link expirado, solicite um novo.',
              duration: 8000,
            });
          }

          this.isLoading = false;
          this.feito = true;
          this.passwordFormField?.reset();
        },
        error:(err)=>{
          this.toast.error({
            detail: 'Algo deu errado',
            summary: 'Por favor tente novamente mais tarde.',
            duration: 8000
          });

          this.isLoading = false;
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

  toLogin(){
    window.open("/login","_self")
  }

}
