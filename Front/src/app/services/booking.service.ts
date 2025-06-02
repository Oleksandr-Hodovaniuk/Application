import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BookingModel } from '../models/booking.model';
import { CreateBookingModel } from '../models/create-booking.model';
import { WorkspaceDropDown } from '../models/workspace-dropdown.model';
import { PatchBookingModel } from '../models/patch-booking.model';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
   private baseUrl = 'http://localhost:5199/api/bookings';

  constructor(private http: HttpClient) {}

  getAllBookings(): Observable<BookingModel[]> {
    return this.http.get<BookingModel[]>(this.baseUrl);
  }
  
  deleteBooking(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  createBooking(dto: CreateBookingModel): Observable<any> {
    return this.http.post<any>(this.baseUrl, dto);
  }
  
  getWorkspaceOptions(): Observable<WorkspaceDropDown[]> {
    return this.http.get<WorkspaceDropDown[]>('http://localhost:5199/api/workspaces/dropdown');
  }
  
  getBookingById(id: number): Observable<PatchBookingModel> {
    return this.http.get<PatchBookingModel>(`http://localhost:5199/api/bookings/${id}`);
  }
  
 patchBooking(id: number, data: Partial<PatchBookingModel>) {
    return this.http.patch(`${this.baseUrl}/${id}`, data);
  }

}