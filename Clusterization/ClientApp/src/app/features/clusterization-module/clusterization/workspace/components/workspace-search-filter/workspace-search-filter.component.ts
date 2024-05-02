import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IWorkspaceFilter } from '../../models/workspaceFilter';

@Component({
  selector: 'app-workspace-search-filter',
  templateUrl: './workspace-search-filter.component.html',
  styleUrls: ['./workspace-search-filter.component.scss']
})
export class WorkspaceSearchFilterComponent {
  @Input() filter:IWorkspaceFilter={
    filterStr:"",
    typeId:undefined
  };

  @Output() sendEvent=new EventEmitter<IWorkspaceFilter>();


  searchStrChanges(str:string){
    this.filter.filterStr=str;
    this.sendEvent.emit(this.filter);
  }

  typeIdChange(id:string){
    this.filter.typeId=id;
    this.sendEvent.emit(this.filter);
  }
}
