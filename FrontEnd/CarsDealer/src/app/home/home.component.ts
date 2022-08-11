import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TitleStrategy } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private http: HttpClient) { }
  ngOnInit() {
  }
  public formData = new FormData();
  ReqJson: any = {};

  uploadFiles(file: any) {
    console.log('file', file)
    for (let i = 0; i < file.length; i++) {
      this.formData.append("file", file[i], file[i]['name']); 
    }
  }

  RequestUpload() {
    const details = {'name': 'Todor'};
    this.formData.append('details', JSON.stringify(details));
    return this.http.post('https://localhost:44375/api/cars/test', this.formData, {headers: {"Accept": "application/json"}}).subscribe();
  }

}
