import { Component, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ICustomerQuotas } from 'src/app/features/quotas/models/responses/customer-quoatas';
import { CustomerQuotasService } from 'src/app/features/quotas/services/customer-quotas.service';

@Component({
  selector: 'app-customer-quotas-list-page',
  templateUrl: './customer-quotas-list-page.component.html',
  styleUrl: './customer-quotas-list-page.component.scss'
})
export class CustomerQuotasListPageComponent implements OnInit{
  quotas:ICustomerQuotas[]=[];

  constructor(private customerQuotasService:CustomerQuotasService,
    private toastr:MyToastrService){}
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
