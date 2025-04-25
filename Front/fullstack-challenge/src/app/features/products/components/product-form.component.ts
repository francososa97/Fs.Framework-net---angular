import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { Product } from "src/app/shared/Model/product.model";
import { createProduct, updateProduct } from "../store/product.actions";

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html'
})
export class ProductFormComponent implements OnInit, OnChanges {
  @Input() visible = false;
  @Input() product: Product | null = null;
  @Output() close = new EventEmitter<boolean>();

  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(0)]],
    stock: [0, [Validators.required, Validators.min(0)]]
  });

  constructor(private fb: FormBuilder, private store: Store) {}

  ngOnInit(): void {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['product']) {
      if (this.product) {
        this.form.patchValue(this.product);
      } else {
        this.form.reset({ name: '', price: 0, stock: 0 });
      }
    }
  }

  save(): void {
    if (this.form.invalid) return;

    const data = { ...this.product, ...this.form.value } as Product;

    if (this.product?.id) {
      this.store.dispatch(updateProduct({ id: this.product.id, product: data }));
    } else {
      this.store.dispatch(createProduct({ product: data }));
    }

    this.close.emit(true);
  }

  cancel(): void {
    this.close.emit(false);
  }
}
