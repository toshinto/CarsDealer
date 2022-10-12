import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarComponent } from 'src/app/feature/cars/cars/car/car.component';
import { MycarsComponent } from 'src/app/feature/cars/cars/mycars/mycars.component';
import { CarEditComponent } from 'src/app/feature/cars/cars/car-edit/car-edit.component';
import { NotificationComponent } from 'src/app/feature/cars/cars/notification/notification.component';
import { OffersComponent } from 'src/app/feature/cars/cars/offers/offers.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AlertModule } from 'src/app/feature/_alert';
import { CarRoutingModule } from './cars-routing.module';
import { JwPaginationModule } from 'jw-angular-pagination';
import { CarDetailsComponent } from './car-details/car-details.component';



@NgModule({
  declarations: [
    CarComponent,
    MycarsComponent,
    CarEditComponent,
    NotificationComponent,
    OffersComponent,
    CarDetailsComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    AlertModule,
    CarRoutingModule,
    JwPaginationModule
  ]
})
export class CarsModule { }
