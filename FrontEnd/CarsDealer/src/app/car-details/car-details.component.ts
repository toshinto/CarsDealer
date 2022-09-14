import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  offerForm: FormGroup;
  offer: boolean = false;
  constructor(private route: ActivatedRoute, private carService: CarService, private fb: FormBuilder) {
    this.route.params.subscribe(res => {
      this.id = res['id'];
      this.carService.getCar(this.id).subscribe(res => {
        this.car = res;
        this.offerForm = this.fb.group({
          'Id': [this.id],
          'Price': ['', [Validators.min(1)]],
        })
      });
      
    })
   }

  ngOnInit(): void {

  }

  makeOffer(){
    console.log(this.offerForm.value);
    this.carService.makeOffer(this.offerForm.value).subscribe(res => {
    this.offerForm.reset();
    this.offer = true;
    });
  }

}
