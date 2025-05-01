// src/app/product.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, map } from 'rxjs';

export interface Product {
  id: number;
  name: string;
  price: number;
}

@Injectable({ providedIn: 'root' })
export class ProductService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5124/api/products';

  getAll(): Observable<{ products: Product[]; appName: string; currency: string }> {
    return this.http.get<Product[]>(this.apiUrl, { observe: 'response' }).pipe(
      map((res: HttpResponse<Product[]>) => ({
        products: res.body ?? [],
        appName: res.headers.get('X-App-Name') ?? '',
        currency: res.headers.get('X-Currency') ?? '',
      }))
    );
  }

  add(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }
}
