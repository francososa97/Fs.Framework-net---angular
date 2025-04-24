import { createAction, props } from '@ngrx/store';
import { Product } from '../../../shared/Model/product.model';

// Load
export const loadProducts = createAction('[Product] Load');
export const loadProductsSuccess = createAction('[Product] Load Success', props<{ products: Product[] }>());
export const loadProductsFailure = createAction('[Product] Load Failure', props<{ error: any }>());

// Create
export const createProduct = createAction('[Product] Create', props<{ product: Product }>());
export const createProductSuccess = createAction(
    '[Product] Create Success',
    props<{ product: Product }>()
  );
export const createProductFailure = createAction('[Product] Create Failure', props<{ error: any }>());

// Update
export const updateProduct = createAction('[Product] Update', props<{ id: number, product: Product }>());
export const updateProductSuccess = createAction(
    '[Product] Update Success',
    props<{ id: number, product: Product }>()
  );
export const updateProductFailure = createAction('[Product] Update Failure', props<{ error: any }>());

// Delete
export const deleteProduct = createAction('[Product] Delete', props<{ id: number }>());
export const deleteProductSuccess = createAction(
    '[Product] Delete Success',
    props<{ id: number }>()
  );
export const deleteProductFailure = createAction('[Product] Delete Failure', props<{ error: any }>());