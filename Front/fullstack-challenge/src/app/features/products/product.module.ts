import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

// PrimeNG Modules
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DropdownModule } from 'primeng/dropdown';

// NGRX
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { productReducer } from './store/product.reducer';
import { ProductEffects } from './store/product.effects';

// Componentes propios
import { ProductPage } from './pages/product.page';
import { ProductFormComponent } from './components/product-form.component';
import { MessageService } from 'primeng/api';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
// Routing
import { ProductRoutingModule } from './product-routing.module';

@NgModule({
    providers: [MessageService],
    declarations: [
        ProductPage,
        ProductFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    ProductRoutingModule,
    ProgressSpinnerModule,

    // PrimeNG
    TableModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    ToastModule,
    ConfirmDialogModule,
    DropdownModule,

    // NGRX
    StoreModule.forFeature('product', productReducer),
    EffectsModule.forFeature([ProductEffects]),
  ]
})
export class ProductModule {}
