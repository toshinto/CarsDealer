import { Component, OnInit } from '@angular/core';
import { Car } from '../../../interfaces/car';
import { CarService } from '../../../services/car.service';

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
    this.carService.getAdminCars().subscribe(cars =>{ 
      this.cars = cars;
    })
  }

  Disapprove(id: number){
    this.carService.disApproveCar(id).subscribe(data => {
      this.isApproved = data.State as boolean;
    });
  }
  
  Approve(id: number){
    this.carService.approveCar(id).subscribe(data => {
      this.fetchCars();
    });
  }

  Delete(id: number){
    this.carService.deleteCarByAdmin(id).subscribe(res => {
      this.fetchCars();
    });
  }


}
