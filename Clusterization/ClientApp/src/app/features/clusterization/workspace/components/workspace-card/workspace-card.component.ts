import { Component, Input } from '@angular/core';
import { ISimpleClusterizationWorkspace } from '../../models/simpleClusterizationWorkspace';

@Component({
  selector: 'app-workspace-card',
  templateUrl: './workspace-card.component.html',
  styleUrls: ['./workspace-card.component.scss']
})
export class WorkspaceCardComponent {
  @Input() workspace:ISimpleClusterizationWorkspace;
}
