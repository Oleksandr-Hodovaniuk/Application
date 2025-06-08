import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AiAssistantResponseModel } from '../models/ai-assistant-response.model';
import { AiAssistantRequestModel } from '../models/ai-assistant-request.model';

@Injectable({
  providedIn: 'root',
})
export class AiAssistantService {
  private readonly apiUrl = 'http://localhost:5199/api/aiasistants';

  constructor(private http: HttpClient) {}

  getAnswer(question: string): Observable<AiAssistantResponseModel> {
    const request: AiAssistantRequestModel = { question };

    return this.http.post<AiAssistantResponseModel>( this.apiUrl, request);
  }
}