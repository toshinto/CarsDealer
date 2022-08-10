import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators  } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private carService: CarService) {
    this.loginForm = this.fb.group({
      'username': ['', [Validators.required]],
      'password': ['', Validators.required]
    })
   }

  ngOnInit(): void {
  }

  login(){
    this.authService.login(this.loginForm.value).subscribe(data => {
      this.authService.saveToken(data['Token']);
      localStorage.setItem('username', data['UserName']);
      this.router.navigate(['/create']);
      //.then(() => {
     //  window.location.reload();
      //});
    });
  }

  get username(){
    return this.loginForm.get('username');
  }

  get password() {
    return this.loginForm.get('password');
  }

}
