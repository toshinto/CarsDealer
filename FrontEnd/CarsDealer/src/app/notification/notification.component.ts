import { Component, OnInit } from '@angular/core';
import { NotificationListDto } from 'src/app/interfaces/NotificationListDto';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  notifications: Array<NotificationListDto>;
  constructor(private carService: CarService) { }

  ngOnInit(): void {
    this.fetchNotifications();
  }

  fetchNotifications(){
    this.carService.notifications().subscribe(notifications =>{ 
      this.notifications = notifications;
    })
  }

  deleteNotification(id: number){
    this.carService.deleteNotifiocation(id).subscribe(() => {
      this.fetchNotifications();
    })
  }

}
