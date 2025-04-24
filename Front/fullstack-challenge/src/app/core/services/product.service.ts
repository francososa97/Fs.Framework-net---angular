import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from 'src/app/shared/Model/product.model';
import { environment } from 'src/environments/environment';
import { ApiResponse } from 'src/app/shared/Model/product.service';
import { Observable, map } from 'rxjs'; // ✅ Importá map de rxjs

@Injectable({ providedIn: 'root' })
export class ProductService {
  private api = `${environment.apiBaseUrl}/Product`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Product[]> {
    return this.http.get<{ data: Product[] }>(this.api).pipe(
      map((response) => response.data)
    );
  }

  getById(id: number): Observable<ApiResponse<Product>> {
    return this.http.get<ApiResponse<Product>>(`${this.api}/${id}`);
  }

  create(product: Product): Observable<ApiResponse<Product>> {
    return this.http.post<ApiResponse<Product>>(this.api, product);
  }

  update(id: number, product: Product): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, product);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
