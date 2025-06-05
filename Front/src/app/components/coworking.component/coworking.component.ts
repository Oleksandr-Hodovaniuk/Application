import { Component, OnInit } from '@angular/core';
import { CoworkingService } from '../../services/coworking.service';
import { CoworkingModel } from '../../models/coworking.model';
import { CommonModule } from '@angular/common';
import { WorkspaceModel } from '../../models/workspace.model';
import { WorkspaceService } from '../../services/workspace.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-coworking',
  imports: [CommonModule],
  templateUrl: './coworking.component.html',
  styleUrls: ['./coworking.component.css']
})
export class CoworkingComponent implements OnInit {
  coworkings: CoworkingModel[] = [];
  selectedWorkspaces: WorkspaceModel[] = [];

  constructor(private coworkingService: CoworkingService, private workspaceService: WorkspaceService, private router: Router) {}

  ngOnInit(): void {
    this.coworkingService.getAllCoworkings().subscribe({
      next: (data) => this.coworkings = data,
      error: (err) => console.error('Error', err)
    });
  }

  onViewDetails(coworkingId: number): void {
    this.router.navigate(['/coworkings', coworkingId]);
  }
  
  onRefresh(): void {
  this.coworkingService.getAllCoworkings().subscribe({
    next: (data) => this.coworkings = data,
    error: (err) => console.error('Error', err)
  });
}
}