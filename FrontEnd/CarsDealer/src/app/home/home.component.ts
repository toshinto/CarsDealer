import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isApproved: boolean = true;
  cars: Array<Car>;
  constructor(private http: HttpClient, private carService: CarService) { }
  ngOnInit() {
    this.carService.getAllCars().subscribe(cars =>{ 
      this.cars = cars;
      console.log(cars);
    })
  }

  Disapprove(){
    this.isApproved = false;
  }
  Approve(){
    this.isApproved = true;
  }

}
