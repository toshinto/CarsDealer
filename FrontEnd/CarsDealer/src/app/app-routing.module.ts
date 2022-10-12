import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from './services/admin.guard';
import { AdminComponent } from './admin/admin.component';
import { AuthGuardService } from './services/auth-guard.service';
import { HomeComponent } from './common/home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';

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
    path: 'admin', component: AdminComponent, canActivate: [AuthGuardService, AdminGuard]
  },
  {
    path: '**', component: NotFoundComponent
  }
];

export const AppRoutingModule = RouterModule.forRoot(routes);
