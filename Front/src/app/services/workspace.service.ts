import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WorkspaceModel } from '../models/workspace.model';

@Injectable({
  providedIn: 'root'
})
export class WorkspaceService {
  private apiUrl = 'http://localhost:5199/api/workspaces';

  constructor(private http: HttpClient) {}

  getAllWorkspaces(): Observable<WorkspaceModel[]> {
    return this.http.get<WorkspaceModel[]>(this.apiUrl);
  }
}