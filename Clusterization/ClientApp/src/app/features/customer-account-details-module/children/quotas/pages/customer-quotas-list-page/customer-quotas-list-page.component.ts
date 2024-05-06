import { Component, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';
import { ICustomerQuotas } from 'src/app/features/shared-module/quotas/models/responses/customer-quotas';
import { CustomerQuotasService } from 'src/app/features/shared-module/quotas/services/customer-quotas.service';

@Component({
  selector: 'app-customer-quotas-list-page',
  templateUrl: './customer-quotas-list-page.component.html',
  styleUrl: './customer-quotas-list-page.component.scss'
})
export class CustomerQuotasListPageComponent implements OnInit{
  quotas:ICustomerQuotas[]=[];

  constructor(private customerQuotasService:CustomerQuotasService,
    private toastr:MyToastrService,
    public accountService:AccountService){}
  ngOnInit(): void {
    this.load();
  }

  load(){
    this.customerQuotasService.getAll().subscribe(res=>{
      this.quotas=res;
    },error=>{
      this.toastr.error(error.error.Message);
    })
  }
}
