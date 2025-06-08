import { AiBookingModel } from "./ai-booking.model";

export interface AiAssistantResponseModel {
  message: string;
  bookings: AiBookingModel[];
}