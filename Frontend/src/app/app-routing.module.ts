import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductComponent } from './add-product/add-product.component';
import { LoginComponent } from './login/login.component';
import { ProductsComponent } from './products/products.component';
import { RegisterComponent } from './register/register.component';
import { RequestsComponent } from './requests/requests.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent, data: {role: "User"} },
  { path: 'register-admin', component: RegisterComponent, data: {role: "Admin"} },
  { path: 'products', component: ProductsComponent },
  { path: 'requests', component: RequestsComponent },
  { path: 'products/add-product', component: AddProductComponent},
  { path: '', redirectTo: 'products', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
