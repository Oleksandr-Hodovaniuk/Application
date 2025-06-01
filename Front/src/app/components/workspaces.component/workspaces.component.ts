import { Component, OnInit } from '@angular/core';
import { WorkspaceModel } from '../../models/workspace.model';
import { WorkspaceService } from '../../services/workspace.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-workspaces.component',
  imports: [CommonModule, RouterModule],
  templateUrl: './workspaces.component.html',
  styleUrl: './workspaces.component.css'
})
export class WorkspacesComponent {
  workspaces: WorkspaceModel[] = [];

  constructor(private workspaceService: WorkspaceService){}

  selectedPictureIndices: number[] = [];

  ngOnInit(): void {
    this.workspaceService.getAllWorkspaces().subscribe(workspaces => {
      this.workspaces = workspaces;
      this.selectedPictureIndices = this.workspaces.map(() => 0);
    });
  }

  selectPicture(workspaceIndex: number, pictureIndex: number): void {
  this.selectedPictureIndices[workspaceIndex] = pictureIndex;
  }
}
