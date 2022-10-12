import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { CarService } from '../../services/car.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy{

  constructor(private authService: AuthService, private carService: CarService, private router: Router) { 
   
  }
  subscription = new Subscription();
  isAdmin: boolean;

  ngOnInit(): void {
    this.carService.isAdmin().pipe(take(1)).subscribe(data => {
      if(data){
        this.isAdmin = data.IsAdmin as boolean;
      }
    })
   
  }

  get isLogged(): boolean{
    if(this.authService.getToken()){
      return true;
    }
    return false;
  }

  logoutHandler(): void{
    localStorage.removeItem('username');
    this.authService.removeToken();
    localStorage.removeItem('admin');
    this.router.navigate(['login']);
  }

  get currentUserName(){
      return localStorage.getItem('username');
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  isUserAdmin(): boolean{
    var isAdmin = localStorage.getItem('admin');
    if(isAdmin == "Admin"){
      return true;
    }
    return false;
  }

  getAdminStatus(): boolean{
    var isAdmin = this.authService.getIsAdminStatus();
    if(isAdmin === "Admin"){
      return true;
    }

    return false;
  }

}
