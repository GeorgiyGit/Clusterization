import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ClusterizationTypesService } from '../../services/clusterization-types.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-clusterization-types-select',
  templateUrl: './clusterization-types-select.component.html',
  styleUrls: ['./clusterization-types-select.component.scss']
})
export class ClusterizationTypesSelectComponent implements OnInit {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable:boolean;

  options: IOptionForSelectInput[] = [];
  constructor(private typesService: ClusterizationTypesService) { }
  ngOnInit(): void {
    this.typesService.getAll().subscribe(res => {
      this.options = [];

      
      if(this.isNullAvailable==true){
        let nullOption:IOptionForSelectInput={
          value:undefined,
          description:'Нічого'
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
