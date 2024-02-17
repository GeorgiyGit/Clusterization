import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { ClusterizationTypesService } from 'src/app/features/clusterization/clusterizationTypes/services/clusterization-types.service';
import { ClusterizationAlgorithmTypesService } from '../../services/clusterization-algorithm-types.service';

@Component({
  selector: 'app-clusterization-algorithm-types-select',
  templateUrl: './clusterization-algorithm-types-select.component.html',
  styleUrls: ['./clusterization-algorithm-types-select.component.scss']
})
export class ClusterizationAlgorithmTypesSelectComponent implements OnInit {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable:boolean;

  tooltip:string=$localize`Тип алгоритму`;

  options: IOptionForSelectInput[] = [];
  constructor(private algorithmTypesService: ClusterizationAlgorithmTypesService) { }
  ngOnInit(): void {
    this.algorithmTypesService.getAll().subscribe(res => {
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
        this.sendEvent.emit(res[0].id);
      }
      
      res.forEach(type => {
        let option: IOptionForSelectInput = {
          value: type.id,
          description: type.name
        };
        this.options.push(option);
      });
    });
  }

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option.value);
  }
}
