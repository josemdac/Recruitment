import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { VerifyEmailComponent } from './components/verify-email/verify-email.component';
import { HomeComponent } from './pages/home/home.component';


// route guard
import { AuthGuard } from './shared/guard/auth.guard';
import { TablasComponent } from './pages/tablas/tablas.component';
import { CreateComponent } from './pages/create/create.component';
import { InformationComponent } from './pages/information/information.component';
import { DetalleComponent } from './pages/detalle/detalle.component';
import { PersonalComponent } from './pages/personal/personal.component'; 
import { ApplicationsComponent } from './components/applications/applications.component';
import { MyapplicationsComponent } from './components/myapplications/myapplications.component';
import { UploadComponent } from './components/upload/upload.component';
import { GraphComponent } from './components/graph/graph.component';
import { AfterapplicationComponent } from './components/afterapplication/afterapplication.component';
import { SmtpComponent } from './pages/smtp/smtp.component';
import { SmtpconfirmationComponent } from './pages/smtpconfirmation/smtpconfirmation.component';
import { LandingComponent } from './pages/landing/landing.component';
import { MyAccountComponent } from './pages/my-account/my-account.component';
import { JobListComponent } from './pages/job-list/job-list.component';
import { JobPositionComponent } from './pages/job-position/job-position.component';
import { RegisterComponent } from './pages/register/register.component';
import { ActivateComponent } from './pages/activate/activate.component';
import { ApplyProcessComponent } from './pages/apply-process/apply-process.component';
import { PrivacyComponent } from './pages/privacy/privacy.component';
import { InterfaceManagementComponent } from './pages/interface-management/interface-management.component';
import { ResetPasswordFormComponent } from './pages/reset-password/reset-password.component';

const routes: Routes = [
  { path: '', redirectTo: '/landing', pathMatch: 'full' },
  { path: 'recruitment', redirectTo: '/landing' },
  { path: 'sign-in', component: SignInComponent },
  { path: 'register-user', component: SignUpComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'verify-email-address', component: VerifyEmailComponent },
  { path: 'home', component: HomeComponent },
  { path: 'tablas', component: TablasComponent },
  { path: 'create', component: CreateComponent },
  { path: 'information', component: InformationComponent },
  { path: 'detalle', component: DetalleComponent },
  { path: 'personal', component: PersonalComponent },
  { path: 'application', component: ApplicationsComponent},
  { path: 'myapplication', component: MyapplicationsComponent},
  { path: 'upload', component: UploadComponent},
  { path: 'graph', component: GraphComponent},
  { path: 'afterapplication', component: AfterapplicationComponent},
  { path: 'smtp', component: SmtpComponent},
  { path: 'smtpconfirmation', component: SmtpconfirmationComponent},
  /* Nuevo desarrollo v2 */
  { path: 'privacy', component: PrivacyComponent},
  { path: 'landing', component: LandingComponent},
  { path: 'joblist', component: JobListComponent},
  { path: 'top10', component: JobListComponent},
  { path: 'register', component: RegisterComponent },
  { path: 'interface-management', component: InterfaceManagementComponent },
  { path: 'jobposition/:requestId', component: JobPositionComponent },
  { path: 'activate/:token', component: ActivateComponent },
  { path: 'reset-password', component: ResetPasswordFormComponent },
  { path: 'myaccount', component: MyAccountComponent, canActivate: [AuthGuard] },
  { path: 'apply-process', component: ApplyProcessComponent, canActivate: [AuthGuard] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})

export class AppRoutingModule {}