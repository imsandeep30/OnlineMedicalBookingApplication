import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-medicine-card',
  standalone: true,
  templateUrl: './medicine-card.html',
  styleUrls: ['./medicine-card.css']
})
export class MedicineCard {
  @Input() medicine: any;
  @Output() remove = new EventEmitter<void>();

  onRemove() {
    this.remove.emit();
  }
}
