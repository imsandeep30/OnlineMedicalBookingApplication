import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
//import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-manage-medicines',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './manage-medicines.html',
  styleUrls: ['./manage-medicines.css']
})
export class ManageMedicines implements OnInit {
  @ViewChild('addForm') addForm!: NgForm;
  @ViewChild('editForm') editForm!: NgForm;
  searchTerm = '';
  selectedStatus = '';
  Math = Math;
  medicines: any[] = [];
  statuses = ['In Stock', 'Out of Stock', 'Low Stock'];
  newMedicine: any = this.getEmptyMedicine();
  editFormData: any = {};
  editingMedicineId: number | null = null;

  constructor(private http: HttpClient) {}

 ngOnInit() {
  this.fetchMedicines();

  // Reset Add form on modal close
  const addModalEl = document.getElementById('addMedicineModal');
  if (addModalEl) {
    addModalEl.addEventListener('hidden.bs.modal', () => {
      this.addForm.resetForm(this.getEmptyMedicine()); // resets values + validation state
    });
  }

  // Reset Edit form on modal close
  const editModalEl = document.getElementById('editMedicineModal');
  if (editModalEl) {
    editModalEl.addEventListener('hidden.bs.modal', () => {
      this.editForm.resetForm(); // Just clear form validation state
    });
  }
}

  getEmptyMedicine() {
    return {
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
            rawName: med.medicineName,
            description: med.description,
            price: med.price,
            stock: med.quantityAvailable,
            status: status
          };
        });
      });
  }

  // ===== Add Medicine Modal =====

  addMedicine() {
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });

    if (this.newMedicine.manufactureDate) {
      this.newMedicine.manufactureDate = new Date(this.newMedicine.manufactureDate).toISOString();
    }
    if (this.newMedicine.expiryDate) {
      this.newMedicine.expiryDate = new Date(this.newMedicine.expiryDate).toISOString();
    }

    this.http.post('http://localhost:5184/api/Medicine/add-medicine', this.newMedicine, { headers })
      .subscribe({
        next: () => {
          alert('Medicine added successfully.');
          this.fetchMedicines();
            const modalEl= document.getElementById('addMedicineModal');
  if (modalEl) {
    modalEl.addEventListener('show.bs.modal', () => {
      this.newMedicine = this.getEmptyMedicine();
    });
  }
        },
        error: (err) => {
          console.error(err);
          alert('Failed to add medicine.');
        }
      });
  }


  // ===== Edit Medicine Modal =====
  openEditModal(medicine: any) {
    this.editingMedicineId = medicine.code;
    this.editFormData = {
      medicineId: medicine.code,
      medicineName: medicine.name.split(" (")[0],
      brand: medicine.name.match(/\(([^)]+)\)/)?.[1] || "",
      price: medicine.price,
      quantityAvailable: medicine.stock,
      description: medicine.description
    };
   
  }



  updateMedicine() {
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });

    this.http.put(
      `http://localhost:5184/api/Medicine/update-medicine/${this.editFormData.medicineId}`,
      this.editFormData,
      { headers }
    ).subscribe({
      next: () => {
        alert('Medicine updated successfully.');
        this.fetchMedicines();
      },
      error: (err) => {
        console.error(err);
        alert('Failed to update medicine.');
      }
    });
  }

  deleteMedicine(medicineName: string,medicineId:number) {
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });

    if (confirm(`Are you sure you want to delete all records for "${medicineName}"?`)) {
      this.http.delete(`http://localhost:5184/api/Medicine/delete-medicine/${medicineId}`, {
        responseType: 'text',
        headers
      }).subscribe({
        next: (res) => {
          alert(res);
          this.fetchMedicines();
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
      return (!this.searchTerm || m.name.toLowerCase().includes(this.searchTerm.toLowerCase())) &&
             (!this.selectedStatus || m.status === this.selectedStatus);
    });
  }
}
