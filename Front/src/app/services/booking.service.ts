import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BookingModel } from '../models/booking.model';
import { CreateBookingModel } from '../models/create-booking.model';
import { WorkspaceDropDown } from '../models/workspace-dropdown.model';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  private apiUrl = 'https://localhost:7102/api/bookings';

  constructor(private http: HttpClient) {}

  getAllBookings(): Observable<BookingModel[]> {
    return this.http.get<BookingModel[]>(this.apiUrl);
  }
  
  deleteBooking(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  createBooking(dto: CreateBookingModel): Observable<any> {
    return this.http.post<any>(this.apiUrl, dto);
  }
  
  getWorkspaceOptions(): Observable<WorkspaceDropDown[]> {
    return this.http.get<WorkspaceDropDown[]>('https://localhost:7102/api/workspaces/dropdown');
  }
}