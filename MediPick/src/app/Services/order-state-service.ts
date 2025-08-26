import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderStateService {
  private originalOrderId: number | null = null;

  setOriginalOrderId(id: number) {
    this.originalOrderId = id;
  }

  getOriginalOrderId(): number | null {
    return this.originalOrderId;
  }

  clear() {
    this.originalOrderId = null;
  }
}
