import { Component, OnInit } from '@angular/core';
import { MyBookingModel } from '../../models/my-bookings.model';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../services/booking.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-bookings',
  imports: [CommonModule, RouterLink],
  templateUrl: './my-bookings.component.html',
  styleUrl: './my-bookings.component.css'
})
export class MyBookingsComponent {
  bookings: MyBookingModel[] = [];

  constructor(private bookingService: BookingService){}

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
}
