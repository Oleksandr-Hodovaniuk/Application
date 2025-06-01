import { RoomInfo } from "./room-info.model";

export interface WorkspaceDropDown{
    id: number;
    type: string;
    name: string;
    maxBookingDuration: number;
    rooms: RoomInfo[];
}