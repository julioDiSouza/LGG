import { Routes } from '@angular/router';
import { ConfirmacaoCadastroComponent } from './components/confirmacao/confirmacao-cadastro.component';
import { HomeComponent } from './components/home/home.component';
import { App } from './app';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'Email/cadastro_confirmacao', component: ConfirmacaoCadastroComponent }
];
