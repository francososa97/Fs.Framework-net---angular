import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductPage } from './pages/product.page';

const routes: Routes = [
  {
    path: '',
    component: ProductPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule {}
