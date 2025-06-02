import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BookingService } from '../../services/booking.service';
import { CreateBookingModel } from '../../models/create-booking.model';
import { WorkspaceDropDown } from '../../models/workspace-dropdown.model';
import { RoomInfo } from '../../models/room-info.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-booking',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './create-booking.component.html',
  styleUrl: './create-booking.component.css'
})
export class CreateBookingComponent implements OnInit {
  bookingForm: FormGroup;

  workspaces: WorkspaceDropDown[] = [];
  availableRooms: RoomInfo[] = [];

  today: string = '';       
  minTime: string = '';

  endDays: number[] = [];
  
  days: number[] = [];
  months = [
    { value: 1, name: 'January' },
    { value: 2, name: 'February' },
    { value: 3, name: 'March' },
    { value: 4, name: 'April' },
    { value: 5, name: 'May' },
    { value: 6, name: 'June' },
    { value: 7, name: 'July' },
    { value: 8, name: 'August' },
    { value: 9, name: 'September' },
    { value: 10, name: 'October' },
    { value: 11, name: 'November' },
    { value: 12, name: 'December' }
  ];
  years: number[] = [];
  hours: string[] = [];
  minutes: string[] = [];

  validationErrors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private bookingService: BookingService,
    private router: Router
  ) {
    this.bookingForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      workspaceType: ['', Validators.required],

      roomId: ['', Validators.required],

      startDay: ['', Validators.required],
      startMonth: ['', Validators.required],
      startYear: ['', Validators.required],
      startHour: ['', Validators.required],
      startMinute: ['', Validators.required],

      endDay: ['', Validators.required],
      endMonth: ['', Validators.required],
      endYear: ['', Validators.required],
      endHour: ['', Validators.required],
      endMinute: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.generateDateTimeDropdowns();

    this.bookingForm.get('startMonth')?.valueChanges.subscribe(() => this.updateDaysForMonthYear('start'));
    this.bookingForm.get('startYear')?.valueChanges.subscribe(() => this.updateDaysForMonthYear('start'));

    this.bookingForm.get('endMonth')?.valueChanges.subscribe(() => this.updateDaysForMonthYear('end'));
    this.bookingForm.get('endYear')?.valueChanges.subscribe(() => this.updateDaysForMonthYear('end'));

    this.bookingService.getWorkspaceOptions().subscribe({
      next: (data) => {
        this.workspaces = data;
      },
      error: (err) => {
        console.error('Error loading workspaces:', err);
      }
    });

    this.bookingForm.get('workspaceType')?.valueChanges.subscribe((selectedWorkspaceId) => {
      const selectedWorkspace = this.workspaces.find(ws => ws.id === +selectedWorkspaceId);
      this.availableRooms = selectedWorkspace?.rooms || [];

      if (this.availableRooms.length) {
        this.bookingForm.get('roomId')?.setValidators(Validators.required);
      } else {
        this.bookingForm.get('roomId')?.clearValidators();
      }

      this.bookingForm.get('roomId')?.updateValueAndValidity();
    });

    this.updateDaysForMonthYear('start');
    this.updateDaysForMonthYear('end');

    const now = new Date();

    this.bookingForm.patchValue({
      startYear: now.getFullYear(),
      startMonth: now.getMonth() + 1,
      startDay: now.getDate(),
      startHour: now.getHours().toString().padStart(2, '0'),
      startMinute: now.getMinutes().toString().padStart(2, '0'),

      endYear: now.getFullYear(),
      endMonth: now.getMonth() + 1,
      endDay: now.getDate(),
      endHour: (now.getHours() + 1).toString().padStart(2, '0'),
      endMinute: now.getMinutes().toString().padStart(2, '0'),
    });
  }

  private generateDateTimeDropdowns() {
    const currentYear = new Date().getFullYear();
    this.years = [currentYear, currentYear + 1];
    this.months = [
      { value: 1, name: 'January' },
      { value: 2, name: 'February' },
      { value: 3, name: 'March' },
      { value: 4, name: 'April' },
      { value: 5, name: 'May' },
      { value: 6, name: 'June' },
      { value: 7, name: 'July' },
      { value: 8, name: 'August' },
      { value: 9, name: 'September' },
      { value: 10, name: 'October' },
      { value: 11, name: 'November' },
      { value: 12, name: 'December' }
    ];
    this.updateDaysForMonthYear('start');
    this.updateDaysForMonthYear('end');
    this.hours = Array.from({ length: 24 }, (_, i) => i.toString().padStart(2, '0'));
    this.minutes = Array.from({ length: 60 }, (_, i) => i.toString().padStart(2, '0'));
  }

  updateDaysForMonthYear(prefix: 'start' | 'end') {
    const yearControl = this.bookingForm.get(`${prefix}Year`);
    const monthControl = this.bookingForm.get(`${prefix}Month`);

    const year = +yearControl?.value || new Date().getFullYear();
    const month = +monthControl?.value || new Date().getMonth() + 1;

    const daysInMonth = new Date(year, month, 0).getDate();
    const daysArray = Array.from({ length: daysInMonth }, (_, i) => i + 1);

    if (prefix === 'start') {
      this.days = daysArray;
    } else if (prefix === 'end') {
      this.endDays = daysArray;
    }
  }

  get selectedWorkspace(): WorkspaceDropDown | undefined {
    const selectedId = +this.bookingForm.get('workspaceType')?.value;
    return this.workspaces.find(w => w.id === selectedId);
  }

  getRoomLabel(capacity: number): string {
    const type = this.selectedWorkspace?.type;
    return type?.toLowerCase() === 'openspace'
      ? `${capacity} ${capacity === 1 ? 'desk' : 'desks'}`
      : `Room for ${capacity} ${capacity === 1 ? 'person' : 'people'}`;
  }

  get roomsForSelectedWorkspace(): RoomInfo[] {
    const selectedId = this.bookingForm.get('workspaceType')?.value;
    const selectedWorkspace = this.workspaces.find(ws => ws.id === +selectedId);
    return selectedWorkspace ? selectedWorkspace.rooms : [];
  }

    onSubmit() {
    if (this.bookingForm.invalid) return;

    this.validationErrors = [];

    const form = this.bookingForm.value;

    // Method to transfer time to local.
    function formatLocalDateTime(date: Date): string {
      const pad = (n: number) => n.toString().padStart(2, '0');

      const year = date.getFullYear();
      const month = pad(date.getMonth() + 1);
      const day = pad(date.getDate());

      const hours = pad(date.getHours());
      const minutes = pad(date.getMinutes());
      const seconds = pad(date.getSeconds());

      return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;
    }

    const startDate = new Date(
      +form.startYear,
      +form.startMonth - 1,
      +form.startDay,
      +form.startHour,
      +form.startMinute
    );

    const endDate = new Date(
      +form.endYear,
      +form.endMonth - 1,
      +form.endDay,
      +form.endHour,
      +form.endMinute
    );

    const startDateTime = formatLocalDateTime(startDate);
    const endDateTime = formatLocalDateTime(endDate);

    const booking: CreateBookingModel = {
      name: form.name,
      email: form.email,
      roomId: form.roomId,
      startDateTime,
      endDateTime
    };

    this.bookingService.createBooking(booking).subscribe({
      next: () => {
        const selectedRoom = this.availableRooms.find(r => r.id === +form.roomId);
        const roomCapacity = selectedRoom?.capacity ?? 0;

        this.onBookingCreatedSuccessfully(
        form.email,
        this.formatDate(startDateTime),
        this.formatDate(endDateTime),
        roomCapacity,
        this.selectedWorkspace?.type ?? ''
      );

    this.bookingForm.reset();
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

  showSuccessModal = false;
  successMessage = '';

  onBookingCreatedSuccessfully(
    email: string,
    start: string,
    end: string,
    people: number,
    workspaceType: string
    ) {
    let bookingSubject: string;

    if (workspaceType === 'OpenSpace') {
      bookingSubject = 'Your desk';
    } else {
      const personWord = people === 1 ? 'person' : 'people';
      bookingSubject = `Your room for ${people} ${personWord}`;
    }

    this.successMessage = `${bookingSubject} is booked from ${start} to ${end}. A confirmation has been sent to your email ${email}.`;
    this.showSuccessModal = true;
  }

  closeSuccessModal() {
    this.showSuccessModal = false;
  }

  goToMyBookings() {
    this.showSuccessModal = false;
    this.router.navigate(['/my-bookings']);
  }

  formatDate(date: string): string {
    const d = new Date(date);
    return d.toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    });
  }
}