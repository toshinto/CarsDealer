import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';
import {MatPaginator, PageEvent} from '@angular/material/paginator'; 



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  cars: Array<Car>;
  lowValue: number = 0;
  highValue: number;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(private http: HttpClient, private carService: CarService) { }
  ngOnInit() {
    this.carService.getAllCars().subscribe(cars =>{ 
      this.cars = cars;
      this.highValue = this.cars.length;
    })
  }

  getPaginationData(event: PageEvent): PageEvent{
    this.lowValue = event.pageIndex * event.pageSize;
    this.highValue = this.lowValue + event.pageSize;
    return event;
  }

 
}
