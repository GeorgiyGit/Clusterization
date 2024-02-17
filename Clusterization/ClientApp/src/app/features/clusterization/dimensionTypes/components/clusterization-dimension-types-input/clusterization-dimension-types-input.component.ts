import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ClusterizationDimensionTypesService } from '../../services/clusterization-dimension-types.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-clusterization-dimension-types-input',
  templateUrl: './clusterization-dimension-types-input.component.html',
  styleUrls: ['./clusterization-dimension-types-input.component.scss']
})
export class ClusterizationDimensionTypesInputComponent implements OnInit{
  @Output() sendEvent=new EventEmitter<number>();

  @Input() isNullAvailable:boolean;
  
  tooltip:string=$localize`Кількість вимірів`;

  options:IOptionForSelectInput[]=[];
  constructor(private typesService:ClusterizationDimensionTypesService){}
  ngOnInit(): void {
      this.typesService.getAll().subscribe(res=>{
        this.options = [];
      
        if(this.isNullAvailable==true){
          let nullOption:IOptionForSelectInput={
            value:undefined,
            description:$localize`Нічого`
          }
  
          this.options.push(nullOption);
  
          this.sendEvent.emit(undefined);
        }
        else { 
          this.sendEvent.emit(res[0].dimensionCount);
        }
        
        res.forEach(type => {
          let option: IOptionForSelectInput = {
            value: type.dimensionCount+"",
            description: type.dimensionCount+""
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
