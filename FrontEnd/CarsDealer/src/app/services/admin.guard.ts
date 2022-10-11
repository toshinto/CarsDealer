import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router){}
  canActivate(): boolean{
    var isAdmin = this.authService.getIsAdminStatus();
    if(isAdmin === "Admin"){
      return true;
    }
    this.router.navigate(['home']);
    return false;
  }
  
}
