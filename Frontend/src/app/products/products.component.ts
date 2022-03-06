import { Component, OnInit } from '@angular/core';
import { UserStateService } from '../_services/user-state.service';
import { ProductsService } from '../_services/products.service';
import { TokenStorageService } from '../_services/token-storage.service';
import { RequestsService } from '../_services/requests.service';
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products: any = [];
  showMore: boolean = false;
  pageSize: number = 5;
  page: number = 1;
  form: { [name: string]: number } = {};
  isSuccess = false;
  constructor(public userStateService: UserStateService, public productsService: ProductsService, private tokenStorageService: TokenStorageService, public requestsService: RequestsService) {
  }

  ngOnInit(): void {
    this.productsService.getProducts(this.page).subscribe({
      next: data => {
        this.products = data.entities;
        this.showMore = data.count > this.pageSize * this.page;
      },
      error: err => {

      }
    });
  }

  createNewRequest(product: any): void {
    const user = this.tokenStorageService.getUser();
    const quantity = this.form[product.id.value]
    console.log(quantity);
    this.requestsService.createRequest(user.userId, product.id, quantity).subscribe({
      next: data => {
        this.isSuccess = data.isSuccess;
      },
      error: err => {
        this.isSuccess = false;
      }
    });
  }

  deleteProduct(product: any): void {
    this.productsService.deleteProduct(product.id.value).subscribe({
      next: data => {
        window.location.reload();
      },
      error: err => {
      }
    });
  }

  loadMore(): void{
    this.page = Math.ceil(this.products.length / this.pageSize) + 1;
    console.log(this.page);
    this.productsService.getProducts(this.page).subscribe({
      next: data => {
        this.products = this.products.concat(data.entities);
        this.showMore = data.count > this.pageSize * this.page;
      },
      error: err => {

      }
    });
  }
}
