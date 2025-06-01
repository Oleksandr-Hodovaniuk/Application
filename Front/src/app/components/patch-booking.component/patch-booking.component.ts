import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../services/booking.service';
import { PatchBookingModel } from '../../models/patch-booking.model';

@Component({
  selector: 'app-patch-booking',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './patch-booking.component.html',
  styleUrls: ['./patch-booking.component.css']
})
export class PatchBookingComponent implements OnInit {
  bookingForm!: FormGroup;
  bookingId!: number;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private bookingService: BookingService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.bookingId = Number(this.route.snapshot.paramMap.get('id'));

    this.bookingForm = this.fb.group({
      name: [''],
      email: ['', [Validators.email]],
      selectedWorkspaceId: [null],
      selectedRoomId: [null],
      startDateTime: [''],
      endDateTime: ['']
    });

    this.bookingService.getBookingById(this.bookingId).subscribe({
      next: (booking: PatchBookingModel) => {
        this.bookingForm.patchValue({
          name: booking.name,
          email: booking.email,
          selectedWorkspaceId: booking.selectedWorkspaceId,
          selectedRoomId: booking.selectedRoomId,
          startDateTime: booking.startDateTime,
          endDateTime: booking.endDateTime
        });
      },
      error: (err) => {
        console.error('Error loading booking', err);
      }
    });
  }

  onSubmit(): void {
    if (this.bookingForm.invalid) {
      return;
    }

    const updatedBooking: Partial<PatchBookingModel> = this.bookingForm.value;

    this.bookingService.patchBooking(this.bookingId, updatedBooking).subscribe({
      next: () => {
        alert('Booking updated successfully');
        this.router.navigate(['/bookings']);
      },
      error: (err) => {
        console.error('Error updating booking', err);
        alert('Error updating booking');
      }
    });
  }
}