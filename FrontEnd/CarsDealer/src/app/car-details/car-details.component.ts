import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-car-details',
  templateUrl: './car-details.component.html',
  styleUrls: ['./car-details.component.css']
})
export class CarDetailsComponent implements OnInit {
  id: number;
  car: Car;
  constructor(private route: ActivatedRoute, private carService: CarService) {
    this.route.params.subscribe(res => {
      this.id = res['id'];
      this.carService.getCar(this.id).subscribe(res => {
        this.car = res;
        console.log(res);
      });
      
    })
   }

  ngOnInit(): void {
  }

}
