import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-mycars',
  templateUrl: './mycars.component.html',
  styleUrls: ['./mycars.component.css']
})
export class MycarsComponent implements OnInit {
  cars: Array<Car>;
  pageOfItems: Array<any>

  constructor(private carService: CarService, private router: Router) { }

  ngOnInit() {
    this.fetchCars();
  }
  
  onChangePage(pageOfItems: Array<any>){
    this.pageOfItems = pageOfItems;
  }

  fetchCars(){
    this.carService.getMyCars().subscribe(cars =>{ 
      this.cars = cars;
    })
  }

  deleteCar(id: number){
    this.carService.deleteCar(id).subscribe(res => {
      this.fetchCars();
    });
  }

  editCar(id: number){
    this.router.navigate(["cars/" + id + "/edit"]);
  }

}
