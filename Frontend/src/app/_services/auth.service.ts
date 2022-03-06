import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

const API_URL = environment.apiURL + 'Users/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post(API_URL + 'login', {
      username,
      password
    }, httpOptions);
  }

  register(username: string, email: string, password: string, role: string): Observable<any> {
    return this.http.post(API_URL + 'register', {
      username,
      email,
      password,
      role
    }, httpOptions);
  }
}
