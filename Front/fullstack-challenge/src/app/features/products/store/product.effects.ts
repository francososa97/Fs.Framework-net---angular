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
  deleteProductFailure
} from './product.actions';
import { MessageService } from 'primeng/api';

@Injectable()
export class ProductEffects {
  constructor(
    private actions$: Actions,
    private productService: ProductService,
    private store: Store,
    private messageService: MessageService
  ) {}

  create$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createProduct),
      switchMap(({ product }) =>
        this.productService.create(product).pipe(
          map(() => {
            this.messageService.add({ severity: 'success', summary: 'Producto creado', detail: 'Se creó correctamente' });
            return createProductSuccess();
          }),
          catchError(error => {
            this.messageService.add({ severity: 'error', summary: 'Error al crear', detail: error.message });
            return of(createProductFailure({ error }));
          })
        )
      )
    )
  );

  update$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateProduct),
      switchMap(({ id, product }) =>
        this.productService.update(id, product).pipe(
          map(() => updateProductSuccess()),
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
          map(() => deleteProductSuccess()),
          catchError(error => of(deleteProductFailure({ error })))
        )
      )
    )
  );
}
