import { Component, Input } from '@angular/core';
import { ISimpleClusterizationWorkspace } from '../../models/simpleClusterizationWorkspace';
import { Router } from '@angular/router';

@Component({
  selector: 'app-workspace-card',
  templateUrl: './workspace-card.component.html',
  styleUrls: ['./workspace-card.component.scss']
})
export class WorkspaceCardComponent {
  @Input() workspace:ISimpleClusterizationWorkspace;
  
  constructor(private router:Router){}

  openFull(){
    this.router.navigateByUrl('workspaces/full/'+this.workspace.id);
  }
}
