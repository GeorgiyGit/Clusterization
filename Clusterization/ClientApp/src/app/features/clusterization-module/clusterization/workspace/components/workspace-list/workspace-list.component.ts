import { Component, Input } from '@angular/core';
import { IWorkspaceFilter } from '../../models/workspaceFilter';
import { ISimpleClusterizationWorkspace } from '../../models/responses/simpleClusterizationWorkspace';

@Component({
  selector: 'app-workspace-list',
  templateUrl: './workspace-list.component.html',
  styleUrls: ['./workspace-list.component.scss']
})
export class WorkspaceListComponent {
  @Input() workspaces:ISimpleClusterizationWorkspace[]=[];
}
