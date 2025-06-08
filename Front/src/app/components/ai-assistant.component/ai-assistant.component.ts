import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AiBookingModel } from '../../models/ai-booking.model';
import { AiAssistantService } from '../../services/ai-assistant.service';
import { AiAssistantResponseModel } from '../../models/ai-assistant-response.model';


@Component({
  standalone: true,
  selector: 'app-ai-assistant',
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './ai-assistant.component.html',
  styleUrls: ['./ai-assistant.component.css'],
})
export class AiAssistantComponent {
  userQuestion: string = '';
  assistantMessage: string = '';
  bookings: AiBookingModel[] = [];
  loading = false;

  constructor(private assistantService: AiAssistantService) {}

  askAssistant() {
    if (!this.userQuestion.trim()) return;

    this.loading = true;
    this.assistantMessage = '';
    this.bookings = [];

    this.assistantService.getAnswer(this.userQuestion).subscribe({
      next: (response: AiAssistantResponseModel) => {
        this.assistantMessage = response.message;
        this.bookings = response.bookings;
        this.loading = false;
      },
      error: err => {
        console.error('AI Assistant error:', err);
        const serverError = err.error?.error ?? err.message ?? 'Unknown error occurred';
        alert(serverError);
        this.loading = false;
      }
    });
  }

  setExample(text: string) {
    this.userQuestion = text;
  }
}