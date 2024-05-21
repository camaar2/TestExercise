import { Component, Output, Input, EventEmitter } from '@angular/core';
import { Shipment } from './shipment.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import {BagService} from '../bag.service'
@Component({
  selector: 'app-shipments',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
      <section class="shipment">
        <h2 class="shipment-number">Shipment Number: {{ shipment.shipmentNumber }}</h2>
        <h3 class="shipment-status">{{ shipment.isFinalized ? 'Finalized' : 'Not Finalized' }}</h3>
        <p class="shipment-airport">{{ shipment.airport }}, {{ shipment.flightNumber }}, {{ shipment.flightDate }}</p>
        <a href="#" (click)="learnMore(shipment.id, $event)">Learn More</a>
      </section>

  `,

})
export class ShipmentsComponent  {
  @Input() shipment!: Shipment;
  @Output() viewDetails: EventEmitter<number> = new EventEmitter<number>();
  constructor(private bagService: BagService) {}
  learnMore(shipmentId: number, event: Event): void {
    event.preventDefault();
    this.viewDetails.emit(shipmentId);
  }
}
