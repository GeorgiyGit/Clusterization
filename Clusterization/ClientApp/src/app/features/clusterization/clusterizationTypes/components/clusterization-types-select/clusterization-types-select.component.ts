import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ClusterizationTypesService } from '../../services/clusterization-types.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-clusterization-types-select',
  templateUrl: './clusterization-types-select.component.html',
  styleUrls: ['./clusterization-types-select.component.scss']
})
export class ClusterizationTypesSelectComponent implements OnInit{
  @Output() sendEvent=new EventEmitter<string>();

  options:IOptionForSelectInput[]=[];
  constructor(private typesService:ClusterizationTypesService){}
  ngOnInit(): void {
      this.typesService.getAll().subscribe(res=>{
        this.options=[];
        res.forEach(type=>{
          let option:IOptionForSelectInput={
            value:type.id,
            description:type.name
          };
          this.options.push(option);
        });
      });
  }

  select(option:IOptionForSelectInput){
    this.sendEvent.emit(option.value);
  }
}
