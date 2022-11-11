import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './services/auth.service';
import { CarComponent } from './feature/cars/cars/car-create/car.component';
import { CarService } from './services/car.service';
import { NotFoundComponent } from './common/not-found/not-found.component';
import { HeaderComponent } from './common/header/header.component';
import { FooterComponent } from './common/footer/footer.component';
import { HomeComponent } from './common/home/home.component';
import { MycarsComponent } from './feature/cars/cars/mycars/mycars.component';
import { CarEditComponent } from './feature/cars/cars/car-edit/car-edit.component';
import { AdminComponent } from './feature/admin/admin/admin.component';
import { NotificationComponent } from './feature/cars/cars/notification/notification.component';
import { OffersComponent } from './feature/cars/cars/offers/offers.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { JwPaginationComponent, JwPaginationModule } from 'jw-angular-pagination';
import {MatDialogModule} from '@angular/material/dialog';
import { ConfirmDialogComponent } from './common/confirm-dialog/confirm-dialog.component';
import { TokenInterceptorService } from './interceptors/token-interceptor.service';
import { HttpErrorInterceptor } from './interceptors/http-error.interceptor';
import { AuthModule } from './auth/auth.module';
import { AlertModule } from './feature/_alert';
import { CarsModule } from './feature/cars/cars/cars.module';
import { AdminModule } from './feature/admin/admin.module';
import { ShortenPipe } from './pipes/shorten.pipe';


@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
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
    CarsModule,
    AdminModule
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
