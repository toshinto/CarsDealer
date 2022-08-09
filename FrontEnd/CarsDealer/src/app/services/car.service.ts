import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Car } from '../models/car';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';
import { R3SelectorScopeMode } from '@angular/compiler';


@Injectable({
  providedIn: 'root'
})
export class CarService {
  private carPath = environment.apiUrl + '/api/cars/create';
  private isUserAdmin = environment.apiUrl + '/api/cars/CheckForAdminRole';
  protected currentUserName: string = '';
  constructor(private http: HttpClient) { }

  create(data: any): Observable<Car>{
    return this.http.post<Car>(this.carPath, data);
  }

  isAdmin(){
    return this.http.get<boolean>(this.isUserAdmin).subscribe(data => {
      
    });
  }


}
