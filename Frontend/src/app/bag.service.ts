import { Injectable } from '@angular/core';
import { Bag } from './bags/bag.model';
import axios, { AxiosResponse } from 'axios';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BagService {
  private baseUrl = 'http://localhost:7055/api/Bags';

  constructor() { }

  getAllBags(): Observable<Bag[]> {
    return new Observable<Bag[]>((observer) => {
      axios.get<Bag[]>(this.baseUrl)
        .then((response: AxiosResponse<Bag[]>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }

  getBagsByShipmentId(shipmentId: number): Observable<Bag[]> {
    return new Observable<Bag[]>((observer) => {
      axios.get<Bag[]>(`${this.baseUrl}?shipmentId=${shipmentId}`)
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
