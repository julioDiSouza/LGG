import { Routes } from '@angular/router';
import { ConfirmacaoCadastroComponent } from './components/confirmacao/confirmacao-cadastro.component';
import { App } from './app';

export const routes: Routes = [
  
  {
    path: 'Email/cadastro_confirmacao',
    component: ConfirmacaoCadastroComponent
  }
];
