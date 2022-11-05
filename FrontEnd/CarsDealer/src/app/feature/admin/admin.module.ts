import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin/admin.component';
import { ShortenPipe } from 'src/app/pipes/shorten.pipe';



@NgModule({
  declarations: [
    AdminComponent,
    ShortenPipe
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
