import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../services/booking.service';
import { PatchBookingModel } from '../../models/patch-booking.model';
import { WorkspaceDropDown } from '../../models/workspace-dropdown.model';
import { RoomInfo } from '../../models/room-info.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-patch-booking',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './patch-booking.component.html',
  styleUrls: ['./patch-booking.component.css']
})
export class PatchBookingComponent implements OnInit {
  bookingForm!: FormGroup;
  bookingId!: number;

  workspaces: WorkspaceDropDown[] = [];
  rooms: RoomInfo[] = [];

  validationErrors: string[] = [];

  selectedWorkspaceType: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private bookingService: BookingService,
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
     // Retrieve booking ID from the route parameters
    this.bookingId = Number(this.route.snapshot.paramMap.get('id'));

     // Initialize the form group with default values and validators
    this.bookingForm = this.fb.group({
      name: [''],
      email: ['', [Validators.email]],
      selectedWorkspaceId: [null, Validators.required],
      selectedRoomId: [null, Validators.required],
      startDateTime: ['', Validators.required],
      endDateTime: ['', Validators.required]
    });

    // Load workspace dropdown options
    this.bookingService.getWorkspaceOptions().subscribe({
      next: (data) => {
        this.workspaces = data;

         // After workspaces are loaded, get booking details to prefill the form
        this.bookingService.getBookingById(this.bookingId).subscribe({
          next: (booking: PatchBookingModel) => {
            this.bookingForm.patchValue({
              name: booking.name,
              email: booking.email,
              selectedWorkspaceId: booking.selectedWorkspaceId,
              selectedRoomId: booking.selectedRoomId,
              startDateTime: this.convertToDatetimeLocal(booking.startDateTime),
              endDateTime: this.convertToDatetimeLocal(booking.endDateTime)
            });

            this.onWorkspaceChange(booking.selectedWorkspaceId);
          },
          error: (err) => console.error('Error loading booking', err)
        });
      },
      error: (err) => console.error('Error loading workspaces', err)
    });

    // When workspace changes, update available rooms
    this.bookingForm.get('selectedWorkspaceId')?.valueChanges.subscribe(workspaceId => {
      this.onWorkspaceChange(workspaceId);
    });
  }

  convertToDatetimeLocal(dateStr?: string): string {
    if (!dateStr) return '';
    const date = new Date(dateStr);
    if (isNaN(date.getTime())) return '';
    return date.toISOString().slice(0, 16);
  }

  formatLocalDateTime(date: Date): string {
    return date.getFullYear() + '-' +
      String(date.getMonth() + 1).padStart(2, '0') + '-' +
      String(date.getDate()).padStart(2, '0') + 'T' +
      String(date.getHours()).padStart(2, '0') + ':' +
      String(date.getMinutes()).padStart(2, '0') + ':' +
      String(date.getSeconds()).padStart(2, '0') + '.' +
      String(date.getMilliseconds()).padStart(3, '0');
  }

  onWorkspaceChange(workspaceId: number | string | null | undefined): void {
    const id = Number(workspaceId);

    if (!workspaceId || isNaN(id)) {
      this.rooms = [];
      this.bookingForm.get('selectedRoomId')?.setValue(null);
      return;
    }

    const selected = this.workspaces.find(w => w.id === id);
    this.rooms = selected?.rooms ?? [];

    const currentRoomId = this.bookingForm.get('selectedRoomId')?.value;
    if (!currentRoomId) {
      this.bookingForm.get('selectedRoomId')?.setValue(null);
    }
  }

  // Returns label for each room based on workspace type and capacity
  getRoomLabel(room: RoomInfo): string {
    if (this.selectedWorkspaceType === 'OpenSpace') {
      return '1 desk';
    } else {
      if (room.capacity === 1) {
        return 'room for 1 person';
      } else {
        return `room for ${room.capacity} people`;
      }
    }
  }

  // Generates JSON Patch operations from the form values
  generatePatchOperations(formValue: any): any[] {
    const patchOps: any[] = [];

    for (const key in formValue) {
      if (formValue.hasOwnProperty(key)) {
        let value = formValue[key];

        if ((key === 'startDateTime' || key === 'endDateTime') && value) {
          const date = new Date(value);
          value = this.formatLocalDateTime(date); // Ensure correct format
        }

        patchOps.push({
          op: 'replace',
          path: `/${key}`,
          value: value
        });
      }
    }

    return patchOps;
  }

  // Handles form submission and sends PATCH request
  onSubmit(): void {
    if (this.bookingForm.invalid) return;

    const formValue = this.bookingForm.value;
    const patchBody = this.generatePatchOperations(formValue);

    this.http.patch(`http://localhost:5199/api/bookings/${this.bookingId}`, patchBody, {
      headers: { 'Content-Type': 'application/json-patch+json' }
    }).subscribe({
      next: () => {
        console.log('Booking updated successfully');
        this.validationErrors = [];
        this.patchSuccessMessage = 'Your booking was updated successfully.';
        this.showPatchSuccessModal = true;
      },
      error: (err) => {
        if (err.status === 400) {
          if (Array.isArray(err.error)) {
            this.validationErrors = err.error;
          } else if (typeof err.error === 'string') {
            this.validationErrors = [err.error];
          } else if (err.error?.title) {
            this.validationErrors = [err.error.title];
          } else if (err.error?.error) {
            this.validationErrors = [err.error.error];
          } else {
            this.validationErrors = ['Bad Request'];
          }
        } else {
          this.validationErrors = ['Unexpected error occurred.'];
        }
      }
    });
  }
  
    handleWorkspaceChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedId = Number(target.value);
    const selectedWorkspace = this.workspaces.find(ws => ws.id === selectedId);
    this.selectedWorkspaceType = selectedWorkspace ? selectedWorkspace.type : null;
  }

  showPatchSuccessModal = false;
  patchSuccessMessage = 'Your booking has been updated successfully.';

  closePatchSuccessModal() {
    this.showPatchSuccessModal = false;
  }

  showPatchModal() {
    this.showPatchSuccessModal = true;
  }

  // Navigates to "My Bookings" page after update
  goToMyBookingsAfterPatch() {
    this.showPatchSuccessModal = false;
    this.router.navigate(['/my-bookings']);
  }

}