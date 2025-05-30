import { RoomModel } from "./room.model";

export interface WorkspaceModel{
    id: number;
    type: string;
    name: string;
    description: string;
    icons: string[];
    pictures: string[];
    rooms: RoomModel[];
}