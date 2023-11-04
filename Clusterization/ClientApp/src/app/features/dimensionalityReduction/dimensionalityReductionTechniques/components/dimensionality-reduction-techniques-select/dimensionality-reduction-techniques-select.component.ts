import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DimensionalityReductionTechniquesService } from '../../services/dimensionality-reduction-techniques.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-dimensionality-reduction-techniques-select',
  templateUrl: './dimensionality-reduction-techniques-select.component.html',
  styleUrls: ['./dimensionality-reduction-techniques-select.component.scss']
})
export class DimensionalityReductionTechniquesSelectComponent implements OnInit {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable:boolean;

  options: IOptionForSelectInput[] = [];
  constructor(private drTechniquesService: DimensionalityReductionTechniquesService) { }
  ngOnInit(): void {
    this.drTechniquesService.getAll().subscribe(res => {
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
