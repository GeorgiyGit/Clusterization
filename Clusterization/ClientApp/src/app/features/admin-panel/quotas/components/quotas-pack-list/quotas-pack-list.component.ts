import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IQuotasPackItem } from '../../../../quotas/models/responses/quotas-pack-item';
import { IQuotasPack } from '../../../../quotas/models/responses/quotas-pack';
import { QuotasPacksService } from 'src/app/features/quotas/services/quotas-packs.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

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
