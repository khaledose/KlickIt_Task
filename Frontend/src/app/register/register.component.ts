import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: any = {
    username: null,
    email: null,
    password: null,
  };
  role: string = "";
  isSuccess = false;
  errorMessage = '';

  constructor(private authService: AuthService, private route:ActivatedRoute, private router:Router) { 
    this.role = route.snapshot.data['role'] 
  }

  ngOnInit(): void {
    
  }

  onSubmit(): void {
    const { username, email, password} = this.form;
    
    this.authService.register(username, email, password, this.role).subscribe({
      next: data => {
        this.isSuccess = data.isSuccess;
      },
      error: err => {
        this.errorMessage = err.error.message;
        this.isSuccess = false;
      }
    });
  }

}
