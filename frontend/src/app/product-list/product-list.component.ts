// src/app/product-list.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService, Product } from '../product.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h2>{{ appName }}</h2>
    <p>Currency: {{ currency }}</p>

    <h3>Add Product</h3>
    <input [(ngModel)]="newProduct.name" placeholder="Name" />
    <input [(ngModel)]="newProduct.price" type="number" placeholder="Price" />
    <button (click)="addProduct()">Add</button>

    <ul>
      <li *ngFor="let product of products">
        {{ product.name }} - {{ product.price }} {{ currency }}
      </li>
    </ul>
  `,
})
export class ProductListComponent {
  products: Product[] = [];
  appName = '';
  currency = '';
  newProduct: Partial<Product> = {};

  constructor(private productService: ProductService) {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getAll().subscribe((data) => {
      this.products = data.products;
      this.appName = data.appName;
      this.currency = data.currency;
    });
  }

  addProduct() {
    if (this.newProduct.name && this.newProduct.price != null) {
      this.productService.add({
        id: 0,
        name: this.newProduct.name,
        price: this.newProduct.price,
      } as Product).subscribe(() => {
        this.newProduct = {};
        this.loadProducts();
      });
    }
  }
}
