import { BookingModel } from "./booking.model";

export interface RoomModel{
    id: number;
    capacity: number;
    quantity: number;
    availability: number;
    bookings: BookingModel[];
}