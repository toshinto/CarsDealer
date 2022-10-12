import { Component, OnInit } from '@angular/core';
import { NotificationListDto } from 'src/app/interfaces/NotificationListDto';
import { OfferListDto } from 'src/app/interfaces/OfferListDto';
import { CarService } from '../../../../services/car.service';

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css']
})
export class OffersComponent implements OnInit {
  offers: Array<OfferListDto>;
  constructor(private carService: CarService) { }

  ngOnInit(): void {
    this.fetchNotifications();
  }

  fetchNotifications(){
    this.carService.offers().subscribe(offers =>{ 
      this.offers = offers;
    })
  }
  
  acceptOffer(id: number){
    this.carService.acceptOffer(id).subscribe(res => {
      this.fetchNotifications();
    })
  }

  declineOffer(id: number){
    this.carService.declineOffer(id).subscribe(res => {
      this.fetchNotifications();
    })
  }
}
