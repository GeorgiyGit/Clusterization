import { Component, Input } from '@angular/core';
import { ISimpleClusterizationWorkspace } from '../../models/simpleClusterizationWorkspace';
import { Router } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';

@Component({
  selector: 'app-workspace-card',
  templateUrl: './workspace-card.component.html',
  styleUrls: ['./workspace-card.component.scss']
})
export class WorkspaceCardComponent {
  @Input() workspace:ISimpleClusterizationWorkspace;
  
  constructor(private router:Router,
    public myLocalStorage: MyLocalStorageService){}

  openFull(){
    this.router.navigateByUrl('workspaces/full/'+this.workspace.id);
  }
}
