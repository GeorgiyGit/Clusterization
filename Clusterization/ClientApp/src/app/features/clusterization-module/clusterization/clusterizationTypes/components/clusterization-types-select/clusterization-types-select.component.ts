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

  @Input() isNullAvailable: boolean;
  @Input() initialType: string;

  typesTooltip: string = $localize`Тип робочого простору`;

  options: IOptionForSelectInput[] = [];
  selectedOption: IOptionForSelectInput;

  constructor(private typesService: ClusterizationTypesService) { }
  ngOnInit(): void {
    this.typesService.getAll().subscribe(res => {
      this.options = [];

      if (this.isNullAvailable == true) {
        let nullOption: IOptionForSelectInput = {
          value: undefined,
          description: $localize`Нічого`
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

      if (this.initialType == null) this.selectedOption = this.options[0];
      else {
        let option = this.options.find(e => e.value == this.initialType);
        if (option != null) this.selectedOption = option;
      }
    });
  }

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option?.value);
  }
}
