import { Routes } from '@angular/router';
import { WorkspacesComponent } from './components/workspaces.component/workspaces.component';
import { MyBookingsComponent } from './components/my-bookings.component/my-bookings.component';

export const routes: Routes = [
    { path: '', redirectTo: '/workspaces', pathMatch: 'full' },
    { path: 'workspaces', component: WorkspacesComponent },
    { path: 'my-bookings', component: MyBookingsComponent },
];
