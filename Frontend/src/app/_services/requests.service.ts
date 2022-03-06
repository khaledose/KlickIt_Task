import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenStorageService } from './token-storage.service';
import { environment } from 'src/environments/environment';

const API_URL = environment.apiURL + 'Requests/';

@Injectable({
  providedIn: 'root'
})
export class RequestsService {

  constructor(private http: HttpClient, private tokenStorageService:TokenStorageService) { }

  createRequest(userId: string, productId: string, quantity: number): Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.tokenStorageService.getToken()}`);
    return this.http.post(API_URL + 'New-Request', {userId, productId, quantity}, { headers: headers });
  }

  getRequests(userId: string, pageNumber: number): Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.tokenStorageService.getToken()}`);
    return this.http.post(API_URL, {pageNumber, userId}, { headers: headers });
  }

  deleteRequest(id: string): Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.tokenStorageService.getToken()}`);
    return this.http.delete(API_URL + `${id}`, { headers: headers });
  }

  updateStatus(id: string, status: number): Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.tokenStorageService.getToken()}`);
    return this.http.patch(API_URL +'Status/'+ `${id}`, {status},{ headers: headers });
  }
}
