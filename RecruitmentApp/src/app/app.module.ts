import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import {NgxPaginationModule} from 'ngx-pagination';

import { AppComponent } from './app.component';
import { AngularFireModule } from '@angular/fire/compat';
import { AngularFireAuthModule } from '@angular/fire/compat/auth';
import { AngularFireStorageModule } from '@angular/fire/compat/storage';
import { AngularFirestoreModule } from '@angular/fire/compat/firestore';
import { AngularFireDatabaseModule } from '@angular/fire/compat/database';
import { environment } from '../environments/environment';

// components
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { VerifyEmailComponent } from './components/verify-email/verify-email.component';

// routing
import { AppRoutingModule } from './app-routing.module';

import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// service
import { AuthService } from './shared/services/auth.service';
import { ArticulosService } from './shared/services/articulos.service';

import { HomeComponent } from './pages/home/home.component';
import { TablasComponent } from './pages/tablas/tablas.component';
import { CreateComponent } from './pages/create/create.component';
import { InformationComponent } from './pages/information/information.component';
import { DetalleComponent } from './pages/detalle/detalle.component';
import { PersonalComponent } from './pages/personal/personal.component';

import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { FormsModule } from '@angular/forms';

import { ShareButtonsModule } from 'ngx-sharebuttons/buttons';
import { ShareIconsModule } from 'ngx-sharebuttons/icons';

import { ReactiveFormsModule } from '@angular/forms';
import { ApplicationsComponent } from './components/applications/applications.component';
import { MyapplicationsComponent } from './components/myapplications/myapplications.component';
import { UploadComponent } from './components/upload/upload.component';
import { GraphComponent } from './components/graph/graph.component';
import { AfterapplicationComponent } from './components/afterapplication/afterapplication.component';
import { SmtpComponent } from './pages/smtp/smtp.component';
import { SmtpconfirmationComponent } from './pages/smtpconfirmation/smtpconfirmation.component';
import { LandingComponent } from './pages/landing/landing.component';
import { LayoutComponent } from './layout/layout.component';
import { SiteHeaderComponent } from './layout/site-header/site-header.component';
import { SiteFooterComponent } from './layout/site-footer/site-footer.component';
import { SiteNavComponent } from './layout/site-nav/site-nav.component';
import { NavItemComponent } from './layout/site-nav/nav-item/nav-item.component';
import { JwtInterceptor } from './shared/helpers/jwt.interceptor';
import { DefaultInfoFieldDirective } from './shared/directives/default-info-field.directive';
import { ShareButtonModule } from 'ngx-sharebuttons/button';
import { LoginComponent } from './components/login/login.component';
import { MyAccountComponent } from './pages/my-account/my-account.component';
import { ExpandTabLayoutComponent } from './components/expand-tab-layout/expand-tab-layout.component';
import { ExpandTabDirective } from './components/expand-tab-layout/expand-tab.directive';
import { ExpandTabComponent } from './components/expand-tab-layout/expand-tab/expand-tab.component';
import { ProfileInfoComponent } from './pages/my-account/profile-info/profile-info.component';
import { FormInputComponent } from './components/form-input/form-input.component';
import { FormInputSelectSourceDirective } from './components/form-input/form-input-select-source.directive';
import { AccountSaveComponent } from './pages/my-account/account-save/account-save.component';
import { ChangePasswordComponent } from './pages/my-account/change-password/change-password.component';
import { PassValidationStepperComponent } from './pages/my-account/change-password/pass-validation-stepper/pass-validation-stepper.component';
import { FormInputPasswordDirective } from './components/form-input/form-input-password.directive';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UploadsModule } from '@progress/kendo-angular-upload';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { FormInputTelDirective } from './components/form-input/form-input-tel.directive';
import { UserDocsComponent } from './pages/my-account/user-docs/user-docs.component';
import { StatusDecPipe } from './shared/pipes/status-dec.pipe';
import { DocTypePipe } from './shared/pipes/doc-type.pipe';
import { FileIconsModule } from 'ngx-file-icons';
import { FormInputFileDirective } from './components/form-input/form-input-file.directive';
import { UserDocEditGridDirective } from './pages/my-account/user-docs/user-doc-edit-grid.directive';
import { StandardModalComponent } from './layout/standard-modal/standard-modal.component';
import { DragModalDirective } from './layout/standard-modal/drag-modal.directive';
import { ExpandTabButtonDirective } from './components/expand-tab-layout/expand-tab/expand-tab-button.directive';
import { EmploymentHistComponent } from './pages/my-account/employment-hist/employment-hist.component'
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { FormInputDateDirective } from './components/form-input/form-input-date.directive';
import { CurrencyFieldValuePipe } from './components/form-input/currency-field-value.pipe';
import { UserEducationComponent } from './pages/my-account/user-education/user-education.component';
import { YnDecPipe } from './shared/pipes/yn-dec.pipe';
import { JobListComponent } from './pages/job-list/job-list.component';
import { JobResultsComponent } from './pages/job-list/job-results/job-results.component';
import { JobSearchBarComponent } from './pages/job-list/job-search-bar/job-search-bar.component';
import { JobCategoriesComponent } from './pages/job-list/job-categories/job-categories.component';
import { NgIdleKeepaliveModule } from '@ng-idle/keepalive';
import { ProfileDecPipe } from './shared/pipes/profile-dec.pipe';
import { JobPositionComponent } from './pages/job-position/job-position.component';
import { InnerContentDirective } from './shared/directives/inner-content.directive';
import { RegisterComponent } from './pages/register/register.component';
import { ActivateComponent } from './pages/activate/activate.component';
import { ApplyProcessComponent } from './pages/apply-process/apply-process.component';
import { ApplyProcessStepDirective } from './pages/apply-process/apply-process-step.directive';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { ApplyProcessPersonalTabComponent } from './pages/apply-process/apply-process-personal-tab/apply-process-personal-tab.component';
import { ApplyProcessDocumentsTabComponent } from './pages/apply-process/apply-process-documents-tab/apply-process-documents-tab.component';
import { PrivacyComponent } from './pages/privacy/privacy.component';
import { PrivacyInfoFieldDirective } from './shared/directives/privacy-info-field.directive';
import { JobTvDirective } from './pages/job-list/job-tv.directive';
import { RequestedPositionsComponent } from './pages/my-account/requested-positions/requested-positions.component';
import { ApplicStatDecPipe } from './shared/pipes/applic-stat-dec.pipe';
import { UserLanguagesComponent } from './pages/my-account/user-languages/user-languages.component';
import { LangProfDecPipe } from './shared/pipes/lang-prof-dec.pipe';
import { LoginGoogleDirective } from './components/login/login-google.directive';
import { LoginMicrosoftDirective } from './components/login/login-microsoft.directive';
import { LoginFacebookDirective } from './components/login/login-facebook.directive';
import { LoginLinkedInDirective } from './components/login/login-linked-in.directive';
import { LoginTwitterDirective } from './components/login/login-twitter.directive';
import { CreateAccountLinkDirective } from './components/login/create-account-link.directive';
import { FormInputMultiselectDirective } from './components/form-input/form-input-multiselect.directive';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ShareBtnCustomDirective } from './shared/directives/share-btn-custom.directive';
import { InterfaceManagementComponent } from './pages/interface-management/interface-management.component';
import { InterfaceManagementOptionsComponent } from './pages/interface-management/interface-management-options/interface-management-options.component';
import { InterfaceManagementToggleComponent } from './pages/interface-management/interface-management-toggle/interface-management-toggle.component';
import { IMHeaderDirective } from './pages/interface-management/directives/imheader.directive';
import { IMLogoDirective } from './pages/interface-management/directives/imlogo.directive';
import { IMNavbarDirective } from './pages/interface-management/directives/imnavbar.directive';
import { IMImageDirective } from './pages/interface-management/directives/imimage.directive';
import { IMContentAreaDirective } from './pages/interface-management/directives/imcontent-area.directive';
import { IMButtonDirective } from './pages/interface-management/directives/imbutton.directive';
import { IMSelTabDirective } from './pages/interface-management/directives/imsel-tab.directive';
import { IMNormalTabDirective } from './pages/interface-management/directives/imnormal-tab.directive';
import { IMHoverTabDirective } from './pages/interface-management/directives/imhover-tab.directive';
import { IMFooterSmallDirective } from './pages/interface-management/directives/imfooter-small.directive';
import { IMTitleSampleDirective } from './pages/interface-management/directives/imtitle-sample.directive';
import { IMStepperDirective } from './pages/interface-management/directives/imstepper.directive';
import { IMLoginComponent } from './pages/interface-management/imlogin/imlogin.component';
import { FormInputRadioDirective } from './components/form-input/form-input-radio.directive';
import { IMSaveButtonComponent } from './pages/interface-management/imsave-button/imsave-button.component';
import { LogoImageDirective } from './layout/site-header/logo-image.directive';
import { ImMainSiteImageDirective } from './pages/interface-management/directives/im-main-site-image.directive';
import { FieldTipDirective } from './shared/directives/field-tip.directive';
import { StepperHeightDirective } from './pages/apply-process/stepper-height.directive';
import { SetElementHeightDirective } from './pages/apply-process/set-element-height.directive';
import { EditorModule } from '@progress/kendo-angular-editor';
import { IMLangButtonDirective } from './pages/interface-management/directives/imlang-button.directive';
import { ApplyProcessQuestionsTabComponent } from './pages/apply-process/apply-process-questions-tab/apply-process-questions-tab.component';
import { ApProcQuestionComponent } from './pages/apply-process/apply-process-questions-tab/ap-proc-question/ap-proc-question.component';
import { IMSwitchBackColorDirective } from './pages/interface-management/directives/imswitch-back-color.directive';
import { ProfileDec2Pipe } from './shared/pipes/profile-dec2.pipe';
import { IsAppliedJobDirective } from './pages/job-list/job-results/is-applied-job.directive';
import { ProfileTypeCellDirective } from './shared/directives/profile-type-cell.directive';
import { ResetPasswordComponent } from './components/login/reset-password/reset-password.component';
import { ResetPasswordFormComponent } from './pages/reset-password/reset-password.component';
import { FooterDirective } from './shared/directives/footer.directive';






@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    SignInComponent,
    SignUpComponent,
    ForgotPasswordComponent,
    VerifyEmailComponent,
    HomeComponent,
    TablasComponent,
    CreateComponent,
    InformationComponent,
    DetalleComponent,
    PersonalComponent,
    ApplicationsComponent,
    MyapplicationsComponent,
    UploadComponent,
    GraphComponent,
    AfterapplicationComponent,
    SmtpComponent,
    SmtpconfirmationComponent,
    LandingComponent,
    LayoutComponent,
    SiteHeaderComponent,
    SiteFooterComponent,
    SiteNavComponent,
    NavItemComponent,
    DefaultInfoFieldDirective,
    LoginComponent,
    MyAccountComponent,
    ExpandTabLayoutComponent,
    ExpandTabDirective,
    ExpandTabComponent,
    ProfileInfoComponent,
    FormInputComponent,
    FormInputSelectSourceDirective,
    AccountSaveComponent,
    ChangePasswordComponent,
    PassValidationStepperComponent,
    FormInputPasswordDirective,
    FormInputTelDirective,
    UserDocsComponent,
    StatusDecPipe,
    DocTypePipe,
    FormInputFileDirective,
    UserDocEditGridDirective,
    StandardModalComponent,
    DragModalDirective,
    ExpandTabButtonDirective,
    EmploymentHistComponent,
    FormInputDateDirective,
    CurrencyFieldValuePipe,
    UserEducationComponent,
    YnDecPipe,
    JobListComponent,
    JobResultsComponent,
    JobSearchBarComponent,
    JobCategoriesComponent,
    ProfileDecPipe,
    JobPositionComponent,
    InnerContentDirective,
    RegisterComponent,
    ActivateComponent,
    ApplyProcessComponent,
    ApplyProcessStepDirective,
    ApplyProcessPersonalTabComponent,
    ApplyProcessDocumentsTabComponent,
    PrivacyComponent,
    PrivacyInfoFieldDirective,
    JobTvDirective,
    RequestedPositionsComponent,
    ApplicStatDecPipe,
    UserLanguagesComponent,
    LangProfDecPipe,
    LoginGoogleDirective,
    LoginMicrosoftDirective,
    LoginFacebookDirective,
    LoginLinkedInDirective,
    LoginTwitterDirective,
    CreateAccountLinkDirective,
    FormInputMultiselectDirective,
    ShareBtnCustomDirective,
    InterfaceManagementComponent,
    InterfaceManagementOptionsComponent,
    InterfaceManagementToggleComponent,
    IMHeaderDirective,
    IMLogoDirective,
    IMNavbarDirective,
    IMImageDirective,
    IMContentAreaDirective,
    IMButtonDirective,
    IMSelTabDirective,
    IMNormalTabDirective,
    IMHoverTabDirective,
    IMFooterSmallDirective,
    IMTitleSampleDirective,
    IMStepperDirective,
    IMLoginComponent,
    FormInputRadioDirective,
    IMSaveButtonComponent,
    LogoImageDirective,
    ImMainSiteImageDirective,
    FieldTipDirective,
    StepperHeightDirective,
    SetElementHeightDirective,
    IMLangButtonDirective,
    ApplyProcessQuestionsTabComponent,
    ApProcQuestionComponent,
    IMSwitchBackColorDirective,
    ProfileDec2Pipe,
    IsAppliedJobDirective,
    ProfileTypeCellDirective,
    ResetPasswordComponent,
    ResetPasswordFormComponent,
    FooterDirective
    
    
  ],
  imports: [
    BrowserModule,
    ShareButtonModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFireAuthModule,
    AngularFirestoreModule,
    AngularFireStorageModule,
    AngularFireDatabaseModule,
    NgIdleKeepaliveModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    NgxPaginationModule,
    Ng2SearchPipeModule,
    FormsModule,
    ShareButtonsModule,
    ShareIconsModule,
    ReactiveFormsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient]
      }
    }),
    GridModule,
    BrowserAnimationsModule,
    UploadsModule,
    InputsModule,
    FileIconsModule,
    DateInputsModule,
    LayoutModule,
    DropDownsModule,
    EditorModule
  ],
  providers: [AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
  exports: [
    ProfileTypeCellDirective,
    ResetPasswordComponent,
    FooterDirective
  ],
})

export class AppModule {}

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json?v=' + (new Date().getTime()));
}