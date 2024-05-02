import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { EmbeddingModelsService } from '../../services/embedding-models.service';

@Component({
  selector: 'app-embedding-models-select',
  templateUrl: './embedding-models-select.component.html',
  styleUrl: './embedding-models-select.component.scss'
})
export class EmbeddingModelsSelectComponent implements OnInit {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable:boolean;

  tooltip:string=$localize`Тип моделі ембедингу`;

  options: IOptionForSelectInput[] = [];
  constructor(private embeddingModelsService: EmbeddingModelsService) { }
  ngOnInit(): void {
    this.embeddingModelsService.getAll().subscribe(res => {
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
    this.sendEvent.emit(option?.value);
  }
}
