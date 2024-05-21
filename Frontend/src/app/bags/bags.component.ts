import { Component, Input } from '@angular/core';
import { Bag } from './bag.model';

@Component({
  selector: 'app-bags',
  template: `
    <div *ngFor="let bag of bags">
      <p>Bag Number: {{ bag.number }}</p>
      <p>Type: {{ bag.type }}</p>
      <p>Total Amount: {{ bag.totalAmount }}</p>
      <p>Total Price: {{ bag.totalPrice }}</p>
    </div>
  `
})
export class BagsComponent {
  @Input() bag!: Bag;
}

