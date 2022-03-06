import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenStorageService } from './token-storage.service';
import { environment } from 'src/environments/environment';

const API_URL = environment.apiURL + 'Products/';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  constructor(private http: HttpClient, private tokenStorageService:TokenStorageService) { }

  getProducts(pageNumber: number): Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.tokenStorageService.getToken()}`);
    return this.http.post(API_URL, {pageNumber}, { headers: headers });
  }

  createProduct(name: string, description: string, price: number): Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.tokenStorageService.getToken()}`);
    return this.http.post(API_URL + 'New-Product', {name, description, price}, { headers: headers });
  }

  deleteProduct(id: string): Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.tokenStorageService.getToken()}`);
    return this.http.delete(API_URL + `${id}`, { headers: headers });
  }
}
