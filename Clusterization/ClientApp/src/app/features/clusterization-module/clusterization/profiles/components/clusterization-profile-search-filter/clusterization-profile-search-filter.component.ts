import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IProfileFilter } from '../../models/profile-filter';

@Component({
  selector: 'app-clusterization-profile-search-filter',
  templateUrl: './clusterization-profile-search-filter.component.html',
  styleUrls: ['./clusterization-profile-search-filter.component.scss']
})
export class ClusterizationProfileSearchFilterComponent {
  @Input() filter:IProfileFilter={
    algorithmTypeId:undefined,
    dimensionCount:undefined,
    embeddingModelId:undefined
  };

  @Output() sendEvent=new EventEmitter<IProfileFilter>();


  dimensionCountChange(count:number){
    this.filter.dimensionCount=count;
    this.sendEvent.emit(this.filter);
  }

  algorithmTypeIdChange(id:string){
    this.filter.algorithmTypeId=id;
    this.sendEvent.emit(this.filter);
  }
  embeddingModelIdChange(id:string){
    this.filter.embeddingModelId=id;
    this.sendEvent.emit(this.filter);
  }
}
