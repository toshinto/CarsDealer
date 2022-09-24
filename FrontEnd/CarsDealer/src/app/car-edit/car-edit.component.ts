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
  public formData = new FormData();

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
      'HorsePower': [''],
      'Kilometeres': ['']
    })

   }

   uploadFiles(file: any) {
    console.log('file', file)
    for (let i = 0; i < file.length; i++) {
      this.formData.append("file", file[i], file[i]['name']); 
    }
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.carId = params['id'];
      this.carService.getCarUpdateDetails(this.carId).subscribe(res => {
        this.car = res;
        this.carForm = this.fb.group({
          'Id': [this.car.Id],
          'Brand': [this.car.Brand, [Validators.required, Validators.pattern(/^[A-Z]+[a-zA-Z0-9]+/)]],
          'Model': [this.car.Model, [Validators.required, Validators.pattern(/^[A-Z]+[a-zA-Z0-9]+/)]],
          'Description': [this.car.Description, [Validators.required]],
          'Fuel': [this.car.Fuel, [Validators.required]],
          'GearLever': [this.car.GearLever, [Validators.required]],
          'Price': [this.car.Price, [Validators.required, Validators.min(1), Validators.max(2000000)]],
          'Year': [this.car.Year,  [Validators.required, Validators.min(1900), Validators.max(2022)]],
          'City': [this.car.City, [Validators.required, Validators.pattern(/^[A-Z]+[a-zA-Z]/)]],
          'Color': [this.car.Color, [Validators.required, Validators.pattern(/^[A-Z]+[a-zA-Z]/)]],
          'Kilometeres': [this.car.Kilometeres, [Validators.required, Validators.min(0), Validators.max(10000000)]],
          'HorsePower': [this.car.HorsePower, [Validators.required, Validators.min(1), Validators.max(1000)]]
        })
      });
    })
  }

  updateCar(){
    this.formData.append('details', JSON.stringify(this.carForm.value));
    this.carService.editCar(this.formData).subscribe(res => {
      this.router.navigate(["myCars"]);
    })
  }
}
