import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './services/auth.service';
import { CarComponent } from './cars/car/car.component';
import { CarService } from './services/car.service';
import { NotFoundComponent } from './not-found/not-found.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { MycarsComponent } from './mycars/mycars.component';
import { CarDetailsComponent } from './car-details/car-details.component';
import { CarEditComponent } from './car-edit/car-edit.component';
import { AdminComponent } from './admin/admin.component';
import { NotificationComponent } from './notification/notification.component';
import { OffersComponent } from './offers/offers.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { JwPaginationModule } from 'jw-angular-pagination';
import {MatDialogModule} from '@angular/material/dialog';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { TokenInterceptorService } from './interceptors/token-interceptor.service';
import { HttpErrorInterceptor } from './interceptors/http-error.interceptor';
import { AuthModule } from './auth/auth.module';
import { AlertModule } from './_alert';


@NgModule({
  declarations: [
    AppComponent,
    CarComponent,
    NotFoundComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    MycarsComponent,
    CarDetailsComponent,
    CarEditComponent,
    AdminComponent,
    NotificationComponent,
    OffersComponent,
    ConfirmDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NoopAnimationsModule,
    JwPaginationModule,
    MatDialogModule,
    AuthModule,
    FormsModule,
    AlertModule,
  ],
  providers: [
    AuthService, 
    CarService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true,
    },
  ],
  exports:[
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
