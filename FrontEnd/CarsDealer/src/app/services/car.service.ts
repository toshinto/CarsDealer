import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Car } from '../models/car';
import { map, switchMap } from 'rxjs/operators';
import { UserAdminDto } from 'src/DTOS/UserAdminDto';
import { ApproveDisapprove } from 'src/DTOS/ApproveDisapproveDto';
import { NotificationListDto } from 'src/DTOS/NotificationListDto';
import { OfferListDto } from 'src/DTOS/OfferListDto';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private carPath = environment.apiUrl + '/api/cars/create';
  private allCars = environment.apiUrl+  '/api/cars/GetAllCars'
  private myCars = environment.apiUrl+ '/api/cars/GetMyCars'
  private carDetails = environment.apiUrl+ '/api/cars/CarDetails'
  private deleteCarUrl = environment.apiUrl+ '/api/cars/DeleteCar';
  private deleteCarByAdminUrl = environment.apiUrl+ '/api/cars/DeleteCarByAdmin';
  private updateCar = environment.apiUrl + '/api/cars/UpdateCar';
  private isUserAdmin = environment.apiUrl + '/api/cars/CheckForAdminRole';
  private admin = environment.apiUrl + '/api/cars/AdminCars';
  private approveCarUrl = environment.apiUrl + '/api/cars/ApproveCar';
  private disApproveCarUrl = environment.apiUrl + '/api/cars/DisApproveCar';
  private carUpdateDetailsUrl = environment.apiUrl + '/api/cars/CarUpdateDetails';
  private offerListUrl = environment.apiUrl + '/api/cars/GetMyOffers';
  private acceptOfferUrl = environment.apiUrl + '/api/cars/AcceptOffer';
  private declineOfferUrl = environment.apiUrl + '/api/cars/DeclineOffer';
  private notificationListUrl = environment.apiUrl + '/api/cars/GetMyNotifications';
  private makeOfferUrl = environment.apiUrl + '/api/cars/MakeOffer';
  protected currentUserName: string = '';
  constructor(private http: HttpClient) { }
  isUserAdminA: boolean;

  create(data: any): Observable<Car>{
    return this.http.post<Car>(this.carPath, data);
  }

  isAdmin(): Observable<UserAdminDto>{
    return this.http.get<UserAdminDto>(this.isUserAdmin);
  }

  getAllCars(): Observable<Array<Car>>{
    return this.http.get<Array<Car>>(this.allCars);
  }

  getMyCars(): Observable<Array<Car>>{
    return this.http.get<Array<Car>>(this.myCars);
  }

  getAdminCars(): Observable<Array<Car>>{
    return this.http.get<Array<Car>>(this.admin);
  }

  getCar(id: number): Observable<Car>{
    return this.http.get<Car>(this.carDetails + '/' + id);
  }

  getCarUpdateDetails(id: number): Observable<Car>{
    return this.http.get<Car>(this.carUpdateDetailsUrl + '/' + id);
  }

  deleteCar(id: number){
    return this.http.delete(this.deleteCarUrl + '/' + id);
  }

  deleteCarByAdmin(id: number){
    return this.http.delete(this.deleteCarByAdminUrl + '/' + id);
  }

  editCar(data: any){
    return this.http.post(this.updateCar, data);
  }

  approveCar(id: number): Observable<ApproveDisapprove>{
    return this.http.post(this.approveCarUrl + '/' + id, {});
  }

  disApproveCar(id: number): Observable<ApproveDisapprove>{
    return this.http.post(this.disApproveCarUrl + '/' + id, {});
  }

  offers(): Observable<Array<OfferListDto>>{
    return this.http.get<Array<OfferListDto>>(this.offerListUrl);
  }

  acceptOffer(id: number){
    return this.http.post(this.acceptOfferUrl + '/' + id, {});
  }

  declineOffer(id: number){
    return this.http.post(this.declineOfferUrl + '/' + id, {});
  }

  notifications() : Observable<Array<NotificationListDto>>{
    return this.http.get<Array<NotificationListDto>>(this.notificationListUrl);
  }

  makeOffer(data: any){
    return this.http.post(this.makeOfferUrl, data);
  }

}
