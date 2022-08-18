import { Component, OnInit } from '@angular/core';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  cars: Array<Car>;
  isApproved: boolean;
  constructor(private carService: CarService) { }
  
  ngOnInit(): void {
    this.fetchCars();
  }
  
  fetchCars(){
    this.carService.getMyCars().subscribe(cars =>{ 
      this.cars = cars;
    })
  }

  Disapprove(id: number){
    this.carService.disApproveCar(id).subscribe(data => {
      this.isApproved = data.State as boolean;
      console.log(data.State);
    });
  }
  
  Approve(id: number){
    this.carService.approveCar(id).subscribe(data => {
      this.isApproved = true;
    });
  }


}
