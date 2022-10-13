import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink, TitleStrategy } from '@angular/router';
import { Car } from 'src/app/interfaces/car';
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
 
  @ViewChild('fileUploader') fileUploader: ElementRef;

  constructor(private fb: FormBuilder, private carService: CarService, private router: Router) {
    this.carForm = this.fb.group({
      'Brand': ['', [Validators.required, Validators.pattern(/^[A-Z]+[a-zA-Z0-9]+/)]],
      'Model': ['', [Validators.required, Validators.pattern(/^[A-Z]+[a-zA-Z0-9]+/)]],
      'Description': ['', [Validators.required]],
      'Fuel': ['', [Validators.required]],
      'GearLever': ['', [Validators.required]],
      'Price': ['', [Validators.required, Validators.min(1), Validators.max(2000000)]],
      'Year': ['', [Validators.required, Validators.min(1900), Validators.max(2022)]],
      'City': ['', [Validators.required, Validators.pattern(/^[A-Z]+[a-zA-Z]/)]],
      'Color': ['', [Validators.required, Validators.pattern(/^[A-Z]+[a-zA-Z]/)]],
      'HorsePower': ['', [Validators.required, Validators.min(1), Validators.max(1000)]],
      'Kilometeres': ['', [Validators.required, Validators.min(0), Validators.max(10000000)]],
      'Picture': ['', [Validators.required]]
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
    this.carService.create(this.formData).subscribe({
      next: () => {
        this.router.navigate(['notifications']);
      },
      error: (err) => {
        this.fileUploader.nativeElement.value = null;
        this.formData.delete('file');
      }
    })
  }

}
