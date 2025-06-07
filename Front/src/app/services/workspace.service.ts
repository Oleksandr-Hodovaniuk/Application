import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WorkspaceModel } from '../models/workspace.model';

@Injectable({
  providedIn: 'root'
})
  export class WorkspaceService {
    private apiUrl = 'http://localhost:5199/api';

    constructor(private http: HttpClient) {}

    getAllWorkspacesByCoworkingId(coworkingId: number): Observable<WorkspaceModel[]> {
    return this.http.get<WorkspaceModel[]>(`${this.apiUrl}/coworkings/${coworkingId}/workspaces`);
  }
}