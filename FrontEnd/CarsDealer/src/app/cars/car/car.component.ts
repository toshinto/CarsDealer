import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TitleStrategy } from '@angular/router';
import { Car } from 'src/app/models/car';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent implements OnInit {
  carForm: FormGroup;
  car: Car;
  
  constructor(private fb: FormBuilder, private carService: CarService) {
    this.carForm = this.fb.group({
      'Brand': ['', Validators.required],
      'Model': ['', Validators.required],
      'Description': ['', Validators.required],
      'Fuel': ['', Validators.required],
      'GearLever': ['', Validators.required],
      'Price': ['', Validators.required],
      'Year': ['', Validators.required],
      'City': ['', Validators.required],
      'Color': ['', Validators.required],
      'ImageFileType': ['', Validators.required]
    })
    
   }

  ngOnInit(): void {
  }

  createCar(){
    this.carService.create(this.carForm.value).subscribe(res => {
      console.log(res);
    })
  }

}
