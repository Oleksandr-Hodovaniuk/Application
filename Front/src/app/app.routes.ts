import { Routes } from '@angular/router';
import { WorkspacesComponent } from './components/workspaces.component/workspaces.component';
import { MyBookingsComponent } from './components/my-bookings.component/my-bookings.component';
import { CreateBookingComponent } from './components/create-booking.component/create-booking.component';
import { PatchBookingComponent } from './components/patch-booking.component/patch-booking.component';

export const routes: Routes = [
    { path: '', redirectTo: '/workspaces', pathMatch: 'full' },
    { path: 'workspaces', component: WorkspacesComponent },
    { path: 'my-bookings', component: MyBookingsComponent },
    { path: 'create-booking', component: CreateBookingComponent },
    { path: 'patch-booking/:id', component: PatchBookingComponent }
];
