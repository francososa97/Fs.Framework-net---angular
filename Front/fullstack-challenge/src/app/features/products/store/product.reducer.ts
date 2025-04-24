import { createReducer, on } from '@ngrx/store';
import {
  loadProducts,
  loadProductsSuccess,
  loadProductsFailure,
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
import { Product } from 'src/app/shared/Model/product.model';

export interface ProductState {
  products: Product[];
  loading: boolean;
  error: any;
}

export const initialState: ProductState = {
  products: [],
  loading: false,
  error: null
};

export const productReducer = createReducer(
  initialState,

  // LOAD
  on(loadProducts, state => ({ ...state, loading: true })),
  on(loadProductsSuccess, (state, { products }) => ({
    ...state,
    loading: false,
    products
  })),
  on(loadProductsFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  })),

  // CREATE
  on(createProduct, state => ({ ...state, loading: true })),
  on(createProductSuccess, state => ({ ...state, loading: false })),
  on(createProductFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  })),

  // UPDATE
  on(updateProduct, state => ({ ...state, loading: true })),
  on(updateProductSuccess, state => ({ ...state, loading: false })),
  on(updateProductFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  })),

  // DELETE
  on(deleteProduct, state => ({ ...state, loading: true })),
  on(deleteProductSuccess, state => ({ ...state, loading: false })),
  on(deleteProductFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  }))
);
