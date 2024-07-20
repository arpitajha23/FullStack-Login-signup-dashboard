import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  MyForm: FormGroup;
  constructor(private router: Router) {
    this.MyForm = new FormGroup({
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
  }

  setCredentials(email: string, password: string) {
    localStorage.setItem('email', email);
    localStorage.setItem('password', password);
  }

  onSubmit() {
    const email = this.MyForm.get('email')?.value;
    const password = this.MyForm.get('password')?.value;

    if (email && password) {
      this.setCredentials(email, password);
    }

    const localemail = localStorage.getItem('email');
    const localpassword = localStorage.getItem('password');

    if (localemail === email && localpassword === password) {
      localStorage.setItem('login_token', 'true');
     // this.toaster.success('successful login');
      this.router.navigate(['dashboards/dash'])
    } 
    else {
      //this.toaster.success('unsuccessful login');
      alert('unsuss')
    }
  }

  forgot(){
    this.router.navigate(['forget']); 
  }
  registration(){
    this.router.navigate(['regi']); 
  }
}
