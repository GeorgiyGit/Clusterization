import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { DimensionalityReductionTechniquesService } from '../../services/dimensionality-reduction-techniques.service';

@Component({
  selector: 'app-dimensionality-reduction-techniques-select',
  templateUrl: './dr-techniques-select.component.html',
  styleUrl: './dr-techniques-select.component.scss'
})
export class DRTechniquesSelectComponent implements OnInit {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable:boolean;

  tooltip:string=$localize`Тип проекції`;
  options: IOptionForSelectInput[] = [];
  constructor(private drTechniquesService: DimensionalityReductionTechniquesService) { }
  ngOnInit(): void {
    this.drTechniquesService.getAll().subscribe(res => {
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
