import { HttpClient, HttpParams } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Car } from '../../interfaces/car';
import { CarService } from '../../services/car.service';
import {MatPaginator, PageEvent} from '@angular/material/paginator'; 
import { MatTableDataSource } from '@angular/material/table';
import { FormControl } from '@angular/forms';
import { debounceTime, mergeMap, startWith, switchMap } from 'rxjs';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  cars: Array<Car>;
  pageOfItems: Array<any>;
  searchControl = new FormControl();
  constructor(private http: HttpClient, private carService: CarService) { }
  ngOnInit() {
    this.searchControl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      switchMap(searchValue => this.carService.getAllCars(searchValue))
      )
      .subscribe(cars =>{
        this.cars = cars;
    });
  }

  onChangePage(pageOfItems: Array<any>){
    this.pageOfItems = pageOfItems;
  }

}
