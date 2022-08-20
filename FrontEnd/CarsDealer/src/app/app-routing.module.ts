import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { AuthGuardService } from './auth-guard.service';
import { CarDetailsComponent } from './car-details/car-details.component';
import { CarEditComponent } from './car-edit/car-edit.component';
import { CarComponent } from './cars/car/car.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { MycarsComponent } from './mycars/mycars.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { NotificationComponent } from './notification/notification.component';
import { OffersComponent } from './offers/offers.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomeComponent
  },
  {
    path: 'home', component: HomeComponent, canActivate: [AuthGuardService]
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'register', component: RegisterComponent
  },
  {
    path: 'create', component: CarComponent, canActivate: [AuthGuardService]
  },
  {
    path: 'allCars', component: HomeComponent, canActivate: [AuthGuardService]
  },
  {
    path: 'myCars', component: MycarsComponent, canActivate: [AuthGuardService]
  },
  {
    path: 'admin', component: AdminComponent, canActivate: [AuthGuardService]
  },
  {
    path: 'cars/:id', component: CarDetailsComponent, canActivate: [AuthGuardService]
  },
  {
    path: 'cars/:id/edit', component: CarEditComponent, canActivate: [AuthGuardService]
  },
  {
    path: 'notifications', component: NotificationComponent, canActivate: [AuthGuardService]
  },
  {
    path: 'offers', component: OffersComponent, canActivate: [AuthGuardService]
  },
  {
    path: '**', component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
