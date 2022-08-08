import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Car} from '../models/car';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private catPath = environment.apiUrl + '/api/cars/create';
  constructor(private http: HttpClient, private authService: AuthService) { }

  create(data: any): Observable<Car>{
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${this.authService.getToken()}`)
    return this.http.post<Car>(this.catPath, data, {headers});
  }
}
