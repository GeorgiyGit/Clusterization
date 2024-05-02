import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { QuotasTypesService } from '../../services/quotas-types.service';

@Component({
  selector: 'app-quotas-types-select',
  templateUrl: './quotas-types-select.component.html',
  styleUrl: './quotas-types-select.component.scss'
})
export class QuotasTypesSelectComponent implements OnInit{
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isActive:boolean=true;

  tooltip:string=$localize`Тип квот`;

  options: IOptionForSelectInput[]=[];

  constructor(private quotasTypesService: QuotasTypesService) { }
  ngOnInit(): void {
    this.load();
  }

  load() {
    this.quotasTypesService.getAllTypes().subscribe(res => {
      this.options = [];

      let nullOption: IOptionForSelectInput = {
        value: undefined,
        description: $localize`Нічого`
      }

      this.options.push(nullOption);

      res.forEach(type => {
        let option: IOptionForSelectInput = {
          value: type.id + "",
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
