import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { ROOT_REDUCERS } from './store/app.reducer';
import { AppEffects } from './store/app.effects';
import { AuthEffects } from './auth/store/auth.effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { CoreModule } from './core/core.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AuthGuard } from './shared/interceptor/auth-guard.service';
import { LoaderInterceptorService } from './shared/interceptor/loader-interceptor.service';
import { AgmCoreModule } from '@agm/core';
import { API_KEY } from './secret';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CoreModule,
    BrowserAnimationsModule,
    StoreModule.forRoot(ROOT_REDUCERS),
    EffectsModule.forRoot([AppEffects, AuthEffects]),
    StoreDevtoolsModule.instrument({
      name: 'Flat Appraisal App',
    }),
    NgbModule,
    FontAwesomeModule,
    AgmCoreModule.forRoot({
      apiKey: API_KEY
    })
  ],
  providers: [
    AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
