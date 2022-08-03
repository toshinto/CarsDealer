import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICar } from './CarTest/car/car';

@Injectable({
  providedIn: 'root'
})

export class CarService {

  readonly carsAPIUrl = "https://localhost:44375/api";
  constructor(private http: HttpClient) { }

  getCars(): Observable<ICar[]>{
    return this.http.get<ICar[]>(this.carsAPIUrl + '/Cars/Cars', {
      
    });
  }
}
