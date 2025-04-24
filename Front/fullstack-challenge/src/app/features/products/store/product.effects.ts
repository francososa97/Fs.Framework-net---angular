import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import { ProductService } from 'src/app/core/services/product.service';
import {
  createProduct,
  createProductSuccess,
  createProductFailure,
  updateProduct,
  updateProductSuccess,
  updateProductFailure,
  deleteProduct,
  deleteProductSuccess,
  deleteProductFailure,
  loadProducts,
  loadProductsSuccess,
  loadProductsFailure
} from './product.actions';
import { MessageService } from 'primeng/api';
import { Product } from 'src/app/shared/Model/product.model';

@Injectable()
export class ProductEffects {
  constructor(
    private actions$: Actions,
    private productService: ProductService,
    private store: Store,
    private messageService: MessageService
  ) {}
  load$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadProducts),
      switchMap(() =>
        this.productService.getAll().pipe(
          map((products) => {
            console.log('📦 Productos cargados:', products);
            return loadProductsSuccess({ products });
          }),
          catchError(error => of(loadProductsFailure({ error })))
        )
      )
    )
  );
  
  create$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createProduct),
      switchMap(({ product }) =>
        this.productService.create(product).pipe(
          map((response) => {
            const created = response.data;
            console.log('✅ Producto creado desde backend:', created);
            return createProductSuccess({ product: created });
          }),
          catchError(error => of(createProductFailure({ error })))
        )
      )
    )
  );
  createSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createProductSuccess),
      map(() => loadProducts())
    )
  );

  update$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateProduct),
      switchMap(({ id, product }) =>
        this.productService.update(id, product).pipe(
          map(() => updateProductSuccess({ id, product })),
          catchError(error => of(updateProductFailure({ error })))
        )
      )
    )
  );

  delete$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteProduct),
      switchMap(({ id }) =>
        this.productService.delete(id).pipe(
          map(() => deleteProductSuccess({ id })),
          catchError(error => of(deleteProductFailure({ error })))
        )
      )
    )
  );
}
