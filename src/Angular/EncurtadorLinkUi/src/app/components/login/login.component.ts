import { ResetPasswordService } from './../../services/reset-password.service';
import { UserStoreService } from './../../services/user-store.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import ValidateForm from 'src/app/helpers/validateForm';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { CredentialResponse } from 'google-one-tap';
import { FacebookService } from 'src/app/services/facebook.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  resetPasswordEmail!: string;
  inputEmail!: string;
  isValidEmail!: boolean;
  isValidLoginEmail!: boolean;
  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa fa-eye-slash';
  clicado: boolean = false;
  clicadoEnviar: boolean = false;
  userInfo: any;
  loggedIn = false;
  isLoadingEntrar = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toast: NgToastService,
    private userStoreService: UserStoreService,
    private resetPasswordService: ResetPasswordService,
    private fbService: FacebookService
  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      senha: ['', Validators.required],
    });

    const script = document.createElement('script');
    script.src = "https://accounts.google.com/gsi/client";
    script.async = true;
    document.head.appendChild(script);

    this.botaoGoogle();
  }

  botaoGoogle(){
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      // @ts-ignore
      google.accounts.id.initialize({
        client_id: '23453760117-s0k68aeohem2lrhfabo198ckikvdstu2.apps.googleusercontent.com',
        callback: this.handleCredentialResponse.bind(this),
        auto_select: true,
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
        window.open("home/?status=loggedin","_self")
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

  onFacebookLogin() {
    this.fbService.login().then(
      (response) => {
        this.fbService.getUserProfile().then(
          (profile) => {
            this.userInfo = profile;
            this.loggedIn = true;
          },
          (error) => {
            console.error('Error fetching user profile:', error);
          }
        ).then(
          (response) => {
            this.isLoadingEntrar = true;
            this.authService.loginWithFacebook(JSON.stringify(this.userInfo)).subscribe({
              next: (res) => {
                this.loginForm.reset();
                this.authService.storeToken(res.accessToken);
                this.authService.storeRefreshToken(res.refreshToken);
                const tokenPayLoad = this.authService.decodedToken();
                this.userStoreService.setNomeForStore(tokenPayLoad.firstname);
                this.userStoreService.setRoleForStore(tokenPayLoad.role);
                window.open("home/?status=loggedin","_self")
                //this.router.navigate(["/home/?status=loggedin"]);
              },
              error: (err) => {
                this.toast.error({
                  detail: 'Erro',
                  summary: 'Falha ao Logar pelo Facebook.',
                  duration: 5000,
                });
                this.isLoadingEntrar = false;
              }
            });
          },
          (error) =>{
            this.toast.error({
              detail: 'Erro',
              summary: 'Falha ao Logar pelo Facebook.',
              duration: 5000,
            });
            this.isLoadingEntrar = false;
          }
        );
      },
      (error) => {
        console.error('Login error:', error);
      }
    )
  }

  onLogout() {
    this.fbService.logout().then(
      () => {
        this.loggedIn = false;
        this.userInfo = null;
      },
      (error) => {
        console.error('Logout error:', error);
      }
    );
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.eyeIcon = this.isText ? 'fa fa-eye' : 'fa fa-eye-slash';
    this.type = this.isText ? 'text' : 'password';
  }

  onLogin() {
    if (this.loginForm.valid && this.checkValidLoginEmail(this.inputEmail)) {
      this.isLoadingEntrar = true;
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
          window.open("home/?status=loggedin","_self")
          //this.router.navigate(["/?status=loggedin"]);
        },
        error: (err) => {
          this.toast.error({
            detail: 'Erro',
            summary: 'Falha ao realizar Login.',
            duration: 5000,
          });
          this.isLoadingEntrar = false;
        },
        complete: () => {
          this.isLoadingEntrar = false;
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

  checkValidLoginEmail(event: string) {
    const value = event;
    const pattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$/;
    this.isValidLoginEmail = pattern.test(value);
    return this.isValidLoginEmail;
  }

  confirmToSend() {
    this.clicado = true;
    if (this.checkValidEmail(this.resetPasswordEmail)) {
      this.clicadoEnviar = true;
      this.resetPasswordService
        .sendResetPasswordLink({ Email: this.resetPasswordEmail })
        .subscribe({
          next: (res) => {
            if(res.status == 200){
              this.toast.success({
                detail: 'Solicitação recebida',
                summary: 'Em breve você receberá uma mensagem, verifique sua caixa de e-mail.',
                duration: 10000,
              });
            }
             
            if(res.status == 404){
              this.toast.warning({
                detail: 'Email não localizado',
                summary: 'Por favor realize o cadastro.',
                duration: 8000,
              });
            }

            this.closeResetSenhaModal();
            this.clicadoEnviar = false;
          },
          error: (err) => {
            this.toast.error({
              detail: 'Algo deu errado',
              summary: 'Por favor tente novamente mais tarde.',
              duration: 8000,
            });
            
            this.closeResetSenhaModal();
            this.clicadoEnviar = false;
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
