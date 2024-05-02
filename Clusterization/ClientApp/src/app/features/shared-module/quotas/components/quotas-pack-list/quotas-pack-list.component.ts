import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IQuotasPack } from '../../models/responses/quotas-pack';
import { QuotasPacksService } from '../../services/quotas-packs.service';

@Component({
  selector: 'app-quotas-pack-list',
  templateUrl: './quotas-pack-list.component.html',
  styleUrl: './quotas-pack-list.component.scss'
})
export class QuotasPackListComponent implements OnInit{
  @Input() quotasPacks: IQuotasPack[] = [];
  @Output() selectEvent = new EventEmitter<IQuotasPack>();

  constructor(private quotasPacksService:QuotasPacksService,
    private toastr:MyToastrService){}
  ngOnInit(): void {
    this.loadPacks();
  }
  loadPacks(){
    this.quotasPacksService.getAllPacks().subscribe(res=>{
      this.quotasPacks=res;
    },error=>{
      this.toastr.error(error.error.Message);
    });
  }

  selectedQuptasPack: IQuotasPack;
  selectQuotasPack(quotasPack: IQuotasPack) {
    if(this.selectedQuptasPack!=null){
      this.selectedQuptasPack.isSelected = false;
    }

    quotasPack.isSelected = true;
    this.selectedQuptasPack = quotasPack;

    this.selectEvent.emit(this.selectedQuptasPack);
  }
}
