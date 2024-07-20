import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../_service/user.service';
import { error } from 'node:console';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {

  signupForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]),
    phoneNumber: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.pattern(/^\d{10}$/)]),
    password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    role: new FormControl('', [Validators.required])
  });

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    if (this.signupForm.valid) {
      const userData = this.signupForm.value;
      this.userService.registerUser(userData).subscribe(
        (response) => {
          console.log('Registration successful:', response);
         
          this.router.navigate(['/login']);
        },
        //error=>console.log("errorr",error)
      );
    }
  }

  login() {
    this.router.navigate(['/login']);
  }
}


