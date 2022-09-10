import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink, TitleStrategy } from '@angular/router';
import { Car } from 'src/app/models/car';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent implements OnInit {
  public formData = new FormData();
  carForm: FormGroup;
  car: Car;
  constructor(private fb: FormBuilder, private carService: CarService, private router: Router) {
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
    })
    
   }

   uploadFiles(file: any) {
    console.log('file', file)
    for (let i = 0; i < file.length; i++) {
      this.formData.append("file", file[i], file[i]['name']); 
    }
  }

  ngOnInit(): void {
  }

  createCar(){
    this.formData.append('details', JSON.stringify(this.carForm.value));
    this.carService.create(this.formData).subscribe(res => {
      console.log(res);
      this.router.navigate(['notifications']);
    })
  }

}
