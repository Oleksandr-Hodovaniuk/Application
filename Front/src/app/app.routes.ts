import { Routes } from '@angular/router';
import { WorkspacesComponent } from './components/workspaces.component/workspaces.component';

export const routes: Routes = [
    { path: '', redirectTo: '/workspaces', pathMatch: 'full' },
    { path: 'workspaces', component: WorkspacesComponent },
];
