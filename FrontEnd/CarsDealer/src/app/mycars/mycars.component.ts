import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ConfirmDialogComponent, ConfirmDialogModel } from '../confirm-dialog/confirm-dialog.component';
import { Car } from '../models/car';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-mycars',
  templateUrl: './mycars.component.html',
  styleUrls: ['./mycars.component.css']
})
export class MycarsComponent implements OnInit {
  cars: Array<Car>;
  pageOfItems: Array<any>
  result: boolean;
  constructor(private carService: CarService, private router: Router, private dialog: MatDialog) { }

  ngOnInit() {
    this.fetchCars();
  }
  
  onChangePage(pageOfItems: Array<any>){
    this.pageOfItems = pageOfItems;
  }

  fetchCars(){
    this.carService.getMyCars().subscribe(cars =>{ 
      this.cars = cars;
    })
  }

  deleteCar(id: number){
        this.carService.deleteCar(id).subscribe(res => {
          this.fetchCars();
        });
  }

  editCar(id: number){
    this.router.navigate(["cars/" + id + "/edit"]);
  }

  confirmDialog(): string {
    const message = `Do you want to delete this car?`;

    const dialogData = new ConfirmDialogModel("Confirm", message);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "600px",
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
    });

    return this.result === true ? "Yes" : "No";
  }

}
