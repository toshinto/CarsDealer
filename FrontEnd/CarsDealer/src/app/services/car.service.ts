import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Car } from '../models/car';
import { map, switchMap } from 'rxjs/operators';
import { UserAdminDto } from 'src/DTOS/UserAdminDto';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private carPath = environment.apiUrl + '/api/cars/create';
  private allCars = environment.apiUrl+  '/api/cars/GetAllCars'
  private myCars = environment.apiUrl+ '/api/cars/GetMyCars'
  private isUserAdmin = environment.apiUrl + '/api/cars/CheckForAdminRole';
  protected currentUserName: string = '';
  constructor(private http: HttpClient) { }
  isUserAdminA: boolean;

  create(data: any): Observable<Car>{
    return this.http.post<Car>(this.carPath, data);
  }

  isAdmin(): Observable<UserAdminDto>{
    return this.http.get<UserAdminDto>(this.isUserAdmin);
  }

  getAllCars(): Observable<Array<Car>>{
    return this.http.get<Array<Car>>(this.allCars);
  }

  getMyCars(): Observable<Array<Car>>{
    return this.http.get<Array<Car>>(this.myCars);
  }

}
