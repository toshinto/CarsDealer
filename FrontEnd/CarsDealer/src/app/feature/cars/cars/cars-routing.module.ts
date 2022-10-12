import { RouterModule, Routes } from "@angular/router";
import { AuthGuardService } from "src/app/services/auth-guard.service";
import { CarDetailsComponent } from "./car-details/car-details.component";
import { CarEditComponent } from "./car-edit/car-edit.component";
import { CarComponent } from "./car/car.component";
import { MycarsComponent } from "./mycars/mycars.component";
import { NotificationComponent } from "./notification/notification.component";
import { OffersComponent } from "./offers/offers.component";

const routes: Routes =[
     { 
        path: 'create', component: CarComponent, canActivate: [AuthGuardService]
      },
      {
        path: 'myCars', component: MycarsComponent, canActivate: [AuthGuardService]
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
]

export const CarRoutingModule = RouterModule.forChild(routes);