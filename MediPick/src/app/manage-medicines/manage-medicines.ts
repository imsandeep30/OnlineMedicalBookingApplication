import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient,HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-manage-medicines',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './manage-medicines.html',
  styleUrls: ['./manage-medicines.css']
})
export class ManageMedicines implements OnInit {
  searchTerm = '';
  selectedCategory = '';
  selectedStatus = '';
  Math = Math;

  medicines: any[] = [];
  categories: string[] = [];
  statuses = ['In Stock', 'Out of Stock', 'Low Stock'];
  editingMedicine: any = null;
  editFormData: any = {};
  showAddForm = false;

newMedicine: any = {
  medicineId: 0,
  medicineName: '',
  brand: '',
  price: 0,
  quantityAvailable: 0,
  manufactureDate: '',
  expiryDate: '',
  description: '',
  prescriptionRequired: false
};

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchMedicines();
  }

  fetchMedicines() {
    this.http.get<any[]>('http://localhost:5184/api/Medicine/all-medicines')
      .subscribe(data => {
        this.medicines = data.map(med => {
          let status = '';
          if (med.quantityAvailable === 0) {
            status = 'Out of Stock';
          } else if (med.quantityAvailable < 50) {
            status = 'Low Stock';
          } else {
            status = 'In Stock';
          }

          return {
            code: med.medicineId,
            name: `${med.medicineName} (${med.brand})`,
            rawName: med.medicineName, // keeping original name for delete API
            description: med.description,
            category: 'General',
            price: med.price,
            discount: 0,
            stock: med.quantityAvailable,
            status: status,
            prescription: 'Not Required'
          };
        });

        this.categories = [...new Set(this.medicines.map(m => m.category))];
      });
  }

  toggleAddForm() {
  this.showAddForm = !this.showAddForm;
  if (this.showAddForm) {
    // Reseting form when opening
    this.newMedicine = {
      medicineId: 0,
      medicineName: '',
      brand: '',
      price: 0,
      quantityAvailable: 0,
      manufactureDate: '',
      expiryDate: '',
      description: '',
      prescriptionRequired: false
    };
  }
}

addMedicine() {
  const token = localStorage.getItem('jwt');
  const headers = new HttpHeaders({
    Authorization: `Bearer ${token}`
  });

  // Converting dates to ISO
  if (this.newMedicine.manufactureDate) {
    this.newMedicine.manufactureDate = new Date(this.newMedicine.manufactureDate).toISOString();
  }
  if (this.newMedicine.expiryDate) {
    this.newMedicine.expiryDate = new Date(this.newMedicine.expiryDate).toISOString();
  }

  this.http.post('http://localhost:5184/api/Medicine/add-medicine', this.newMedicine, { headers })
    .subscribe({
      next: (res) => {
        alert('Medicine added successfully.');
        this.fetchMedicines();
        this.toggleAddForm();
      },
      error: (err) => {
        console.error(err);
        alert('Failed to add medicine.');
      }
    });
}

  startEdit(medicine: any) {
  this.editingMedicine = medicine.code;
  // Pre-filling form with current values
  this.editFormData = {
    medicineId: medicine.code,
    medicineName: medicine.name.split(" (")[0], // removing brand from display name
    brand: medicine.name.match(/\(([^)]+)\)/)?.[1] || "",
    price: medicine.price,
    quantityAvailable: medicine.stock,
    description: medicine.description
  };
}

cancelEdit() {
  this.editingMedicine = null;
  this.editFormData = {};
}

updateMedicine() {
  const token = localStorage.getItem('jwt');
  const headers = new HttpHeaders({
    Authorization: `Bearer ${token}`
  });

  this.http.put(
    `http://localhost:5184/api/Medicine/update-medicine/${this.editFormData.medicineId}`,
    this.editFormData,
    { headers }
  ).subscribe({
    next: (res) => {
      alert('Medicine updated successfully.');
      this.fetchMedicines();
      this.cancelEdit();
    },
    error: (err) => {
      console.error(err);
      alert('Failed to update medicine.');
    }
  });
}

deleteMedicine(medicineName: string) {
  const token = localStorage.getItem('jwt');
  const headers = new HttpHeaders({
    Authorization: `Bearer ${token}`
  });

  if (confirm(`Are you sure you want to delete all records for "${medicineName}"?`)) {
    this.http.delete(`http://localhost:5184/api/Medicine/delete-medicine/${medicineName}`, {
      responseType: 'text',
      headers
    }).subscribe({
      next: (res) => {
        alert(res); // showing exact msg from backend
        this.fetchMedicines(); // refreshing table
      },
      error: (err) => {
        console.error(err);
        alert('Failed to delete medicine.');
      }
    });
  }
}


  get filteredMedicines() {
    return this.medicines.filter(m => {
      return (!this.searchTerm || m.name.toLowerCase().includes(this.searchTerm.toLowerCase()) || m.category.toLowerCase().includes(this.searchTerm.toLowerCase())) &&
             (!this.selectedCategory || m.category === this.selectedCategory) &&
             (!this.selectedStatus || m.status === this.selectedStatus);
    });
  }
}
