import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      'username': ['', [Validators.required, Validators.pattern(/\w{1}[a-zA-Z0-9]{3}\w*/)]],
      'email': ['', [Validators.required, Validators.pattern(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)]],
      'password': ['', [Validators.required, Validators.minLength(4)]]
    })
   }

  ngOnInit(): void {
  }

  register(){
     this.authService.register(this.registerForm.value).subscribe(data => {
      console.log(data);
     });
     this.router.navigate(['/login']);
  }

}
