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
  colors = [
  '#a3d9a5', '#f7f3b2', '#c2f0fc', '#e7cbf5', '#f4b0b0', '#a9c5ba',
  '#5a3e36', '#2f4f4f', '#4b0082', '#800000', '#355e3b', '#3b3b6d',
  '#6b4226', '#4a4a4a', '#5c4033', '#3e5f8a', '#7b3f00', '#403d58',
  '#2c3e50', '#34495e', '#1b2631', '#4a235a', '#7d6608', '#784212',
  '#512e5f', '#1c2833', '#283747', '#6e2c00', '#4d5656', '#6c3483',
  '#6e2f2f', '#1b4f72', '#4a235a', '#7e5109', '#5d6d7e', '#3a3b3c'
];

getMedicineColor(name: string): string {
  const index = name.charCodeAt(0) % this.colors.length;
  return this.colors[index];
}

  onRemove() {
    this.remove.emit();
  }

}
