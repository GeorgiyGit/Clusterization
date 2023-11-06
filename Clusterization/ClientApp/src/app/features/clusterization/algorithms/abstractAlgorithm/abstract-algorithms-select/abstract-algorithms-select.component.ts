import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { GeneralClusterizationAlgorithmsService } from '../services/general-clusterization-algorithms.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-abstract-algorithms-select',
  templateUrl: './abstract-algorithms-select.component.html',
  styleUrls: ['./abstract-algorithms-select.component.scss']
})
export class AbstractAlgorithmsSelectComponent implements OnInit, OnChanges {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable: boolean;
  @Input() isActive:boolean=false;

  @Input() typeId: string;

  options: IOptionForSelectInput[] = [];
  constructor(private generalAlgorithmsService: GeneralClusterizationAlgorithmsService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['typeId'] && !changes['typeId'].firstChange) {
      if (this.typeId == undefined) {
        let nullOption: IOptionForSelectInput = {
          value: undefined,
          description: 'Нічого'
        }
        this.options = [nullOption];
        return;
      }
      this.load();
    }
  }
  ngOnInit(): void {
    if (this.typeId == undefined) return;

    this.load();
  }

  load() {
    this.generalAlgorithmsService.getAlgorithms(this.typeId).subscribe(res => {
      this.options = [];

      if (this.isNullAvailable == true) {
        let nullOption: IOptionForSelectInput = {
          value: undefined,
          description: 'Нічого'
        }

        this.options.push(nullOption);
        this.sendEvent.emit(undefined);
      }
      else {
        this.sendEvent.emit(res[0].id + "");
      }

      res.forEach(type => {
        let option: IOptionForSelectInput = {
          value: type.id + "",
          description: type.fullTitle
        };
        this.options.push(option);
      });

      this.isActive=true;
    });
  }

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option.value);
  }
}
