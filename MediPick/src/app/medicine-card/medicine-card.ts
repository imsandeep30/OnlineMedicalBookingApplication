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

  // Generate image path dynamically based on medicine name
  imageUrl(medicneName : string): string {
    if (!this.medicine || !medicneName) {
      return '/duck.png'; // fallback
    }
    // Build image URL from public folder (case-sensitive for file names!)
    return `Assets/${this.medicine.medicneName.replace(/\s+/g, '')}.jpg`;
  }
  onImageError(event: Event) {
    const imgElement = event.target as HTMLImageElement;
    imgElement.src = '/duck.png';
  }
  onRemove() {
    this.remove.emit();
  }
}
