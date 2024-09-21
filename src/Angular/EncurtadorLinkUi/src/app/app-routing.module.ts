import { HomeComponent } from './components/home/home.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

// Import components that will be routed
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AdminComponent } from './components/admin/admin.component';
import { AuthGuard } from './guards/auth.guard';
import { ResetComponent } from './components/reset/reset.component';
import { AboutComponent } from './components/about/about.component';
import { MyComponent } from './components/my/my.component';

// Define routes
const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'home', component: HomeComponent },
  { path: 'my', component: MyComponent, canActivate: [AuthGuard]  },
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuard] },
  { path: 'reset', component: ResetComponent },
  { path: 'about', component: AboutComponent }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
