import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-car-edit',
  templateUrl: './car-edit.component.html',
  styleUrls: ['./car-edit.component.css']
})
export class CarEditComponent implements OnInit {
  carForm: FormGroup;
  carId: number;
  car: Car;
  constructor(private fb: FormBuilder, private route: ActivatedRoute, private carService: CarService, private router: Router) {
     this.carForm = this.fb.group({
      'Id': [''],
      'Brand': [''],
      'Model': [''],
      'Description': [''],
      'Fuel': [''],
      'GearLever': [''],
      'Price': [''],
      'Year': [''],
      'City': [''],
      'Color': [''],
    })

   }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.carId = params['id'];
      this.carService.getCar(this.carId).subscribe(res => {
        this.car = res;
        this.carForm = this.fb.group({
          'Id': [this.car.Id],
          'Brand': [this.car.Brand],
          'Model': [this.car.Model],
          'Description': [this.car.Description],
          'Fuel': [this.car.Fuel],
          'GearLever': [this.car.GearLever],
          'Price': [this.car.Price],
          'Year': [this.car.Year],
          'City': [this.car.City],
          'Color': [this.car.Color],
        })
      });
    })
  }

  updateCar(){
    this.carService.editCar(this.carForm.value).subscribe(res => {
      this.router.navigate(["myCars"]);
    })
  }
}
