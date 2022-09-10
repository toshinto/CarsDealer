import { HttpClient, HttpParams } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';
import {MatPaginator, PageEvent} from '@angular/material/paginator'; 
import { MatTableDataSource } from '@angular/material/table';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  cars: Array<Car>;
  pageOfItems: Array<any>;
  constructor(private http: HttpClient, private carService: CarService) { }
  ngOnInit() {
    this.carService.getAllCars().subscribe(cars =>{ 
      this.cars = cars;
    })
  }

  onChangePage(pageOfItems: Array<any>){
    this.pageOfItems = pageOfItems;
  }

}
