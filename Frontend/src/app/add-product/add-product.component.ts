import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../_services/products.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  form: any = {
    name: null,
    description: null,
    price: 0.0,
  };
  isSuccess = false;
  errorMessage = '';
  constructor(public productsService:ProductsService) { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    console.log(this.form);
    const { name, description, price} = this.form;
    
    this.productsService.createProduct(name, description, price).subscribe({
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
