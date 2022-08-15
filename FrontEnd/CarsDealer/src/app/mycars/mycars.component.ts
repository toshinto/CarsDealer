import { Component, OnInit } from '@angular/core';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-mycars',
  templateUrl: './mycars.component.html',
  styleUrls: ['./mycars.component.css']
})
export class MycarsComponent implements OnInit {
  cars: Array<Car>;

  constructor(private carService: CarService) { }

  ngOnInit() {
    this.carService.getMyCars().subscribe(cars =>{ 
      this.cars = cars;
      console.log(cars);
    })
  }

}
