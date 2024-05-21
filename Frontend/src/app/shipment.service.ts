import { Injectable } from '@angular/core';
import {Bag, Shipment } from './shipments/shipment.model';
import axios, { AxiosResponse } from 'axios';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShipmentService {

  private baseUrl = 'http://localhost:7055/api/Shipments';
  bags: Bag[] = [];
  constructor() { }

  getAllShipments(): Observable<Shipment[]> {
    return new Observable<Shipment[]>((observer) => {
      axios.get<Shipment[]>(this.baseUrl)
        .then((response: AxiosResponse<Shipment[]>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }

  getShipmentById(id: number): Observable<Shipment> {
    return new Observable<Shipment>((observer) => {
      axios.get<Shipment>(`${this.baseUrl}/${id}`)
        .then((response: AxiosResponse<Shipment>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }
  createShipment(shipmentData: Shipment): Observable<any> {
    return new Observable<any>((observer) => {
      axios.post(`${this.baseUrl}/createShipment`, shipmentData)
        .then((response: AxiosResponse<any>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }
  getBagsByShipmentId(id: number): Observable<Bag[]> {
    return new Observable<Bag[]>((observer) => {
      axios.get<Bag[]>(`${this.baseUrl}/${id}/bags`)
        .then((response: AxiosResponse<Bag[]>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }
}
