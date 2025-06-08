import { Component, OnInit } from '@angular/core';
import { WorkspaceModel } from '../../models/workspace.model';
import { WorkspaceService } from '../../services/workspace.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-workspaces.component',
  imports: [CommonModule, RouterModule],
  templateUrl: './workspaces.component.html',
  styleUrl: './workspaces.component.css'
})
export class WorkspacesComponent {
  // Array to hold all workspaces retrieved from the server
  workspaces: WorkspaceModel[] = [];

  // Array to track selected image index for each workspace (used in image gallery)
  selectedPictureIndices: number[] = [];

  coworkingId!: number;

    constructor(private workspaceService: WorkspaceService, private route: ActivatedRoute){}

  // Lifecycle hook that runs after component initialization
  ngOnInit(): void {
    this.coworkingId = Number(this.route.snapshot.paramMap.get('id'));

    this.workspaceService.getAllWorkspacesByCoworkingId(this.coworkingId).subscribe(workspaces => {
      this.workspaces = workspaces;
      this.selectedPictureIndices = this.workspaces.map(() => 0);
    });
  }
  
  // Method to change selected picture for a specific workspace (e.g., carousel logic)
  selectPicture(workspaceIndex: number, pictureIndex: number): void {
  this.selectedPictureIndices[workspaceIndex] = pictureIndex;
  }
}
