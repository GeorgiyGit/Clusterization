import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { GeneralClusterizationAlgorithmsService } from '../services/general-clusterization-algorithms.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { IPageParameters } from 'src/app/core/models/page-parameters';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-abstract-algorithms-select',
  templateUrl: './abstract-algorithms-select.component.html',
  styleUrls: ['./abstract-algorithms-select.component.scss']
})
export class AbstractAlgorithmsSelectComponent implements OnInit, OnChanges {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable: boolean;
  @Input() isActive: boolean = false;

  @Input() typeId: string;

  tooltip: string = $localize`Алгоритм`;

  pageParameters: IPageParameters = {
    pageSize: 10,
    pageNumber: 1
  }

  options: IOptionForSelectInput[] = [];
  constructor(private generalAlgorithmsService: GeneralClusterizationAlgorithmsService,
    private toastr:MyToastrService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['typeId'] && !changes['typeId'].firstChange) {
      if (this.typeId == undefined) {
        let nullOption: IOptionForSelectInput = {
          value: undefined,
          description: $localize`Нічого`
        }
        this.options = [nullOption];
        return;
      }
      this.loadFirst();
    }
  }
  ngOnInit(): void {
    if (this.typeId == undefined) return;

    this.loadFirst();
  }

  loadFirst() {
    this.pageParameters.pageNumber=1;

    this.isMoreActive=false;
    this.generalAlgorithmsService.getAlgorithms(this.typeId, this.pageParameters).subscribe(res => {
      if(res.length<this.pageParameters.pageSize)this.isMoreActive=false;
      else this.isMoreActive=true;

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
        this.sendEvent.emit(res[0].id + "");
      }

      res.forEach(type => {
        let option: IOptionForSelectInput = {
          value: type.id + "",
          description: type.fullTitle
        };
        this.options.push(option);
      });
    },error=>{
      this.toastr.error(error.error.Message);
    });
  }

  isLoadingMore: boolean;
  isMoreActive: boolean;
  loadMore() {
    if (!this.isMoreActive) return;
    this.pageParameters.pageNumber++;

    this.isLoadingMore=true;
    this.generalAlgorithmsService.getAlgorithms(this.typeId, this.pageParameters).subscribe(res => {
      if(res.length<this.pageParameters.pageSize)this.isMoreActive=false;
      else this.isMoreActive=true;

      this.isLoadingMore=false;

      res.forEach(type => {
        let option: IOptionForSelectInput = {
          value: type.id + "",
          description: type.fullTitle
        };
        this.options.push(option);
      });
    },error=>{
      this.isLoadingMore=false;
      this.toastr.error(error.error.Message);
    });
  }

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option?.value);
  }
}
