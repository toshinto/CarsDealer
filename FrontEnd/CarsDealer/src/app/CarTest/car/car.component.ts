import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CarService } from 'src/app/car.service';
import { ICar } from './car';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css'],

})

export class CarComponent implements OnInit {

  carsList$!: Observable<ICar[]>;
  constructor(private carService: CarService) { }

  ngOnInit(): void {
    this.carsList$ = this.carService.getCars();
  }

}
