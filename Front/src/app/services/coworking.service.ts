import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CoworkingModel } from '../models/coworking.model';

@Injectable({
  providedIn: 'root'
})
export class CoworkingService {
  private readonly baseUrl = 'http://localhost:5199/api/coworkings';

  constructor(private http: HttpClient) {}

  getAllCoworkings(): Observable<CoworkingModel[]> {
    return this.http.get<CoworkingModel[]>(this.baseUrl);
  }
}