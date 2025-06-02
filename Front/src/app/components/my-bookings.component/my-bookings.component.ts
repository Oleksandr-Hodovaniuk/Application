import { Component, OnInit } from '@angular/core';
import { MyBookingModel } from '../../models/my-bookings.model';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../services/booking.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-bookings',
  imports: [CommonModule, RouterLink],
  templateUrl: './my-bookings.component.html',
  styleUrl: './my-bookings.component.css'
})
export class MyBookingsComponent {
  bookings: MyBookingModel[] = [];

  showConfirmModal: boolean = false;
  selectedBookingId: number | null = null;

  constructor(private bookingService: BookingService, private router: Router){}

  ngOnInit() {
  this.bookingService.getAllBookings().subscribe({
    next: (data) => {
      this.bookings = data;
    },
    error: (err) => {
      if (err.error && err.error.message) {
        alert(`Error: ${err.error.message}`);
      } else {
        alert('Unknown error.');
      }
      console.error('Booking error.', err);
      }
    });
  }

  deleteBooking(id: number) {
  this.bookingService.deleteBooking(id).subscribe({
    next: () => {
      this.bookings = this.bookings.filter(b => b.id !== id);
    },
    error: (err) => {
      console.error('Error deleting booking:', err);
    }
    });
  }

  onDeleteClick(id: number) {
    this.selectedBookingId = id;
    this.showConfirmModal = true;
  }

  // When confirm.
  confirmDelete() {
    if (this.selectedBookingId !== null) {
      this.bookingService.deleteBooking(this.selectedBookingId).subscribe({
        next: () => {
          this.bookings = this.bookings.filter(b => b.id !== this.selectedBookingId);
          this.showConfirmModal = false;
          this.selectedBookingId = null;
        },
        error: (err) => {
          console.error('Error deleting booking:', err);
          this.showConfirmModal = false;
        }
      });
    }
  }

  cancelDelete() {
    this.selectedBookingId = null;
    this.showConfirmModal = false;
  }

  goToEditBooking(id: number) {
    this.router.navigate(['/bookings/edit', id]);
  }
} 
