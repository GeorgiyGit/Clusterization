import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID,NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS,HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {LayoutModule} from '@angular/cdk/layout';
import { AppComponent } from './app.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './core/routing/app-routing.module';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { CustomHttpInterceptor } from './core/interceptors/custom-http.interceptor';
import { HomePageComponent } from './core/components/home-page/home-page.component';
import { MainHeaderComponent } from './core/components/headers/main-header/main-header.component';
import { YoutubeHeaderComponent } from './core/components/headers/youtube-header/youtube-header.component';
import { MainLayoutComponent } from './core/components/layouts/main-layout/main-layout.component';
import { YoutubeLayoutComponent } from './core/components/layouts/youtube-layout/youtube-layout.component';
import { TelegramLayoutComponent } from './core/components/layouts/telegram-layout/telegram-layout.component';
import { TelegramHeaderComponent } from './core/components/headers/telegram-header/telegram-header.component';
import { ExternalDataLayoutComponent } from './core/components/layouts/external-data-layout/external-data-layout.component';
import { ExternalDataHeaderComponent } from './core/components/headers/external-data-header/external-data-header.component';
import { CloseOutsideDirective } from './core/directives/close-outside.directive';

export function tokenGetter() {
  return localStorage.getItem("user-token");
}
@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,

    MainHeaderComponent,
    YoutubeHeaderComponent,
    TelegramHeaderComponent,
    ExternalDataHeaderComponent,

    MainLayoutComponent,
    YoutubeLayoutComponent,
    TelegramLayoutComponent,
    ExternalDataLayoutComponent,

    CloseOutsideDirective
  ],
  imports:[
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    AppRoutingModule,
    FormsModule,
    RouterModule,
    MatTooltipModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    LayoutModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: [
          "sladkovskygeorge.website",
        ]
      },
    }),
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    JwtHelperService,
    {
      provide:HTTP_INTERCEPTORS,
      useClass: CustomHttpInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
