export interface Bag {
  id: number;
  bagNumber: string | null;
  parcels: Parcel[] | null;
  letterCount: number | null;
  parcelCount: number | null;
  bagWeight: number | null;
  bagPrice: number | null;
}

export interface Parcel {
  id: number;
  parcelNumber: string | null;
  recipientName: string | null;
  destinationCountry: string | null;
  weight: number;
  price: number;
}
export interface Shipment {
  id: number;
  shipmentNumber: string;
  airport: string;
  flightNumber: string;
  flightDate: Date;
  bags: Bag[];
  isFinalized: boolean;
}
