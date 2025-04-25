import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Product } from '../../../shared/Model/product.model';
import { loadProducts, deleteProduct } from '../store/product.actions';
import { selectProductLoading, selectProducts } from '../store/product.selectors';

@Component({
  selector: 'app-product-page',
  templateUrl: './product.page.html'
})
export class ProductPage implements OnInit {
  products$: Observable<Product[]> = this.store.select(selectProducts);
  loading$ = this.store.select(selectProductLoading);
  displayForm = false;
  selectedProduct: Product | null = null;

  constructor(private store: Store) {}

  ngOnInit(): void {
    this.store.dispatch(loadProducts());
  }

  openNew(): void {
    this.selectedProduct = null;
    this.displayForm = true;
  }

  editProduct(product: Product): void {
    this.selectedProduct = product;
    this.displayForm = true;
  }

  deleteProduct(id: number): void {
    this.store.dispatch(deleteProduct({ id }));
  }

  onFormClose(refresh: boolean): void {
    this.displayForm = false;
    this.selectedProduct = null;
    if (refresh) {
      this.store.dispatch(loadProducts());
    }
  }

  onGlobalFilter(event: Event, table: any): void {
    const input = event.target as HTMLInputElement;
    table.filterGlobal(input.value, 'contains');
  }
}
