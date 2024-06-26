import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ClusterizationDimensionTypesService } from '../../services/clusterization-dimension-types.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-clusterization-dimension-types-input',
  templateUrl: './clusterization-dimension-types-input.component.html',
  styleUrls: ['./clusterization-dimension-types-input.component.scss']
})
export class ClusterizationDimensionTypesInputComponent implements OnInit, OnChanges {
  @Output() sendEvent = new EventEmitter<number>();

  @Input() isNullAvailable: boolean;
  @Input() embeddingModelId: string | undefined;
  @Input() initialType: number;

  tooltip: string = $localize`Кількість вимірів`;

  options: IOptionForSelectInput[] = [];
  selectedOption: IOptionForSelectInput;

  constructor(private typesService: ClusterizationDimensionTypesService) { }
  ngOnInit(): void {
    if (this.embeddingModelId == null) return this.loadAllDimensionTypes();
    else this.loadDimensionTypesByEmbeddingModel();
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['embeddingModelId'] && !changes['embeddingModelId'].firstChange) {
      if (this.embeddingModelId == null) return this.loadAllDimensionTypes();
      else this.loadDimensionTypesByEmbeddingModel();
    }
  }

  loadAllDimensionTypes() {
    this.typesService.getAll().subscribe(res => {
      this.options = [];

      if (this.isNullAvailable == true) {
        let nullOption: IOptionForSelectInput = {
          value: undefined,
          description: $localize`Нічого`
        }

        this.options.push(nullOption);

        if (this.initialType == undefined) {
          this.sendEvent.emit(undefined);
        }
      }
      else {
        this.sendEvent.emit(res[0].dimensionCount);
      }

      res.forEach(type => {
        let option: IOptionForSelectInput = {
          value: type.dimensionCount + "",
          description: type.dimensionCount + ""
        };
        this.options.push(option);
      });

      if (this.initialType == null) this.selectedOption = this.options[0];
      else {
        let option = this.options.find(e => parseInt(e.value ?? '-1') == this.initialType);
        if (option != null) this.selectedOption = option;
      }
    });
  }

  loadDimensionTypesByEmbeddingModel() {
    if (this.embeddingModelId == null) return;

    this.typesService.getAllByEmbeddingModel(this.embeddingModelId).subscribe(res => {
      this.options = [];

      if (this.isNullAvailable == true) {
        let nullOption: IOptionForSelectInput = {
          value: undefined,
          description: $localize`Нічого`
        }

        this.options.push(nullOption);

        if (this.initialType == undefined) {
          this.sendEvent.emit(undefined);
        }
      }
      else {
        console.log(321);
        this.sendEvent.emit(res[0].dimensionCount);
      }

      res.forEach(type => {
        let option: IOptionForSelectInput = {
          value: type.dimensionCount + "",
          description: type.dimensionCount + ""
        };
        this.options.push(option);
      });
    });
  }

  select(option: IOptionForSelectInput) {
    if (option != null && option.value != null) {
      var id = parseInt(option.value);
      this.sendEvent.emit(id);
    }
    else if (this.isNullAvailable) this.sendEvent.emit(undefined);
  }
}
