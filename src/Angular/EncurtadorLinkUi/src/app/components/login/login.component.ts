import { ResetPasswordService } from './../../services/reset-password.service';
import { UserStoreService } from './../../services/user-store.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import ValidateForm from 'src/app/helpers/validateForm';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';
import { response } from 'express';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;
  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa fa-eye-slash';
  public resetPasswordEmail!: string;
  public isValidEmail!: boolean;
  clicado: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toast: NgToastService,
    private userStoreService: UserStoreService,
    private resetPasswordService: ResetPasswordService,
    private ngZone: NgZone
  ) {}

  ngOnInit(): void {
    this.botaoGoogle();

    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      senha: ['', Validators.required],
    });
  }

  botaoGoogle(){
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      // @ts-ignore
      google.accounts.id.initialize({
        client_id: '23453760117-s0k68aeohem2lrhfabo198ckikvdstu2.apps.googleusercontent.com',
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true,
      });

      // @ts-ignore
      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById('buttonGoogle'),
        {
          type: 'icon',
          shape: 'circle',
          theme: 'outline',
          size: 'large'
        }
      );

      //// @ts-ignore
      //google.accounts.id.prompt((notification: PromptMomentNotification) => {});
    };
  }

  async handleCredentialResponse(response: CredentialResponse) {
    await this.authService.loginWithGoogle(response.credential).subscribe({
      next: (res) => {
        this.loginForm.reset();
          this.authService.storeToken(res.accessToken);
          this.authService.storeRefreshToken(res.refreshToken);
          const tokenPayLoad = this.authService.decodedToken();
          this.userStoreService.setNomeForStore(tokenPayLoad.firstname);
          this.userStoreService.setRoleForStore(tokenPayLoad.role);
          this.toast.success({
            detail: 'Sucesso',
            summary: 'Você está conectado.',
            duration: 5000,
          });
          window.open("/admin","_self")
          //this.router.navigate(['admin']);
      },
      error: (err) => {
        this.toast.error({
          detail: 'Erro',
          summary: 'Falha ao Logar pelo google.',
          duration: 5000,
        });
      }
    });
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.eyeIcon = this.isText ? 'fa fa-eye' : 'fa fa-eye-slash';
    this.type = this.isText ? 'text' : 'password';
  }

  onLogin() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: (res) => {
          this.loginForm.reset();
          this.authService.storeToken(res.accessToken);
          this.authService.storeRefreshToken(res.refreshToken);
          const tokenPayLoad = this.authService.decodedToken();
          this.userStoreService.setNomeForStore(tokenPayLoad.firstname);
          this.userStoreService.setRoleForStore(tokenPayLoad.role);
          this.toast.success({
            detail: 'Sucesso',
            summary: 'Você está conectado.',
            duration: 5000,
          });
          this.router.navigate(['admin']);
        },
        error: (err) => {
          this.toast.error({
            detail: 'Erro',
            summary: 'Falha ao realizar Login.',
            duration: 5000,
          });
        },
      });
    } else {
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }

  checkValidEmail(event: string) {
    this.clicado = true;
    const value = event;
    const pattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$/;
    this.isValidEmail = pattern.test(value);
    return this.isValidEmail;
  }

  confirmToSend() {
    this.clicado = true;

    if (this.checkValidEmail(this.resetPasswordEmail)) {
      this.resetPasswordService
        .sendResetPasswordLink({ Email: this.resetPasswordEmail })
        .subscribe({
          next: (res) => {
            this.toast.success({
              detail: 'Email enviado',
              summary: 'Verifique sua caixa de e-mail.',
              duration: 5000,
            });
            this.closeResetSenhaModal();
          },
          error: (err) => {
            this.toast.error({
              detail: 'Algo deu errado',
              summary: 'Por favor tente novamente mais tarde.',
              duration: 5000,
            });
            this.closeResetSenhaModal();
          },
        });
    }
  }

  closeResetSenhaModal() {
    const buttonRef = document.getElementById('closeBtn');
    buttonRef?.click();
    this.resetPasswordEmail = '';
    this.clicado = false;
  }

}
