import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';
import { AlertService } from '../_alert';

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
  isOfferShown: boolean = false;
  constructor(private route: ActivatedRoute, private carService: CarService, private fb: FormBuilder, private alertService: AlertService) {
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
      if(res === true){
        this.offerForm.reset();
        this.offer = true;
        this.alertService.success("You have successfully send an offer.");
      }
      else{
        this.alertService.error("You are the owner of this car, so you can not send offers to yourself.")
      }
    });
  }

  isClicked(){
    this.isOfferShown = !this.isOfferShown;
  }

}
