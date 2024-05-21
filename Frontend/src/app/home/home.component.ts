import { Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ShipmentsComponent} from '../../app/shipments/shipments.component'
import { Shipment } from '../shipments/shipment.model';
import {ShipmentService} from '../shipment.service'
import {Bag} from '../bags/bag.model'

@Component({
  selector: 'app-home',
  standalone: true,
  imports:[
    CommonModule,
    ShipmentsComponent
  ],
  template: `
      <div class="banner-container">
        <img src="assets/banner_uus2.png" alt="Banner" class="banner">
      </div>
      <div class="home-content">
      <h1>Welcome to my PostOffice App</h1>
      <p>This application allows you to manage shipments, bags, and parcels in a post office.</p>
      <div class="shipment-image-container">
        <img src="assets/2.png" alt="Shipment Image" class="shipment-image">
      </div>
      </div>
      <section class="results">
        <div *ngFor="let shipment of shipmentList">
          <div class="shipment" (click)="toggleDetails(shipment.id)">
            <h2>Shipment Number: {{ shipment.shipmentNumber }}</h2>
            <p>Is Finalized: {{ shipment.isFinalized ? 'Yes' : 'No' }}</p>
            <div *ngIf="expandedShipmentId === shipment.id">
              <p>Airport: {{ shipment.airport }}</p>
              <p>Flight Number: {{ shipment.flightNumber }}</p>
              <p>Flight Date: {{ shipment.flightDate }}</p>
              <div *ngFor="let bag of shipment.bags">
                <h3>Bag Number: {{ bag.bagNumber }}</h3>
                <p>Letter/Parcel Count: {{ bag.letterCount }} {{bag.parcelCount}}</p>
                <p>Bag Weight: {{ bag.bagWeight }}</p>
                <p>Bag Price: {{ bag.bagPrice }}</p>
              </div>
            </div>
          </div>
        </div>
      </section>
  `,
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  shipmentList: Shipment[] = [];
  expandedShipmentId: number | null = null;
  selectedShipmentId: number | null = null;
  bags: Bag[] = [];

  constructor(private shipmentService: ShipmentService) {
  }

  ngOnInit(): void {
    this.shipmentService.getAllShipments().subscribe(
      (data) => {
        this.shipmentList = data;
      },
      (error) => {
        console.error('Error fetching shipments:', error);
      }
    );
  }

  toggleDetails(shipmentId: number): void {
    this.expandedShipmentId = this.expandedShipmentId === shipmentId ? null : shipmentId;
  }

  collapseDetails(): void {
    this.expandedShipmentId = null;
  }

  closeDetails(): void {
    this.expandedShipmentId = null;
  }

  showBags(shipmentId: number): void {
    if (this.selectedShipmentId === shipmentId) {
      this.selectedShipmentId = null;
    } else {
      this.selectedShipmentId = shipmentId;
      this.shipmentService.getBagsByShipmentId(shipmentId).subscribe(
        (bags) => {
          this.bags = bags;
        },
        (error) => {
          console.error('Error fetching bags:', error);
        }
      );
    }
  }
}

