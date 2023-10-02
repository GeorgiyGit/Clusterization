import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ClusterizationDimensionTypesService } from '../../services/clusterization-dimension-types.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-clusterization-dimension-types-input',
  templateUrl: './clusterization-dimension-types-input.component.html',
  styleUrls: ['./clusterization-dimension-types-input.component.scss']
})
export class ClusterizationDimensionTypesInputComponent implements OnInit{
  @Output() sendEvent=new EventEmitter<number>();

  options:IOptionForSelectInput[]=[];
  constructor(private typesService:ClusterizationDimensionTypesService){}
  ngOnInit(): void {
      this.typesService.getAll().subscribe(res=>{
        this.options=[];
        res.forEach(type=>{
          let option:IOptionForSelectInput={
            value:type.dimensionCount+"",
            description:type.dimensionCount+"",
          };
          this.options.push(option);
        });
      });
  }

  select(option:IOptionForSelectInput){
    if(option.value!=null){
      var id = parseInt(option.value);
      this.sendEvent.emit(id);
    }
  }
}
