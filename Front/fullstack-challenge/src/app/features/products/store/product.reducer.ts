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
    products,
    error: null
  })),
  on(loadProductsFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  })),
  on(loadProductsSuccess, (state, { products }) => ({
    ...state,
    loading: false,
    products,
    error: null
  })),

  // CREATE
  on(createProduct, state => ({ ...state, loading: true })),
  on(createProductSuccess, (state, { product }) => ({
    ...state,
    loading: false,
    products: [...state.products, product],
    error: null
  })),
  on(createProductFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  })),

  // UPDATE
  on(updateProduct, state => ({ ...state, loading: true })),
  on(updateProductSuccess, (state, { id, product }) => ({
    ...state,
    loading: false,
    products: state.products.map(p => p.id === id ? { ...p, ...product } : p),
    error: null
  })),
  on(updateProductFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  })),

  // DELETE
  on(deleteProduct, state => ({ ...state, loading: true })),
  on(deleteProductSuccess, (state, { id }) => ({
    ...state,
    loading: false,
    products: state.products.filter(p => p.id !== id),
    error: null
  })),
  on(deleteProductFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  }))
);
