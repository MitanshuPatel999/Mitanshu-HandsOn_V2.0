import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { NgFor, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgFor,NgIf],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  products: Product[]=[];

  // constructor(private productsService: ApiService){}

  // ngOnInit(): void {
  //   this.loadProducts();
  // }

  // loadProducts(): void {
  //   this.productsService.getProducts().subscribe((data)=>{this.products=data},(error)=>{console.error(error)})
  // }
  constructor(http: HttpClient) {
    http.get<Product[]>("http://localhost:5069/MMcontroller").subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }

  
}

interface Product{
  id: number;
  productName: string;
  price: number;
}