import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { ISimpleClusterizationWorkspace } from '../../models/responses/simpleClusterizationWorkspace';
import { Router } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

@Component({
  selector: 'app-workspace-card',
  templateUrl: './workspace-card.component.html',
  styleUrls: ['./workspace-card.component.scss']
})
export class WorkspaceCardComponent implements OnInit{
  @Input() workspace:ISimpleClusterizationWorkspace;
  
  isYour:boolean=false;
  constructor(private router:Router,
    public myLocalStorage: MyLocalStorageService,
    public accountService:AccountService){}

  ngOnInit(): void {
    let id = this.accountService.getUserId();
    if(id!=null && this.workspace!=null && id==this.workspace.ownerId){
      this.isYour=true;
    }
  }

  openFull(){
    this.router.navigateByUrl('main-layout/clusterization/workspaces/full/'+this.workspace.id);
  }
}
