import { Component, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetQuotasLogsRequest } from 'src/app/features/shared-module/quotas/models/requests/get-quotas-logs-request';
import { IQuotasLogs } from 'src/app/features/shared-module/quotas/models/responses/quotas-logs';
import { QuotasLogsService } from 'src/app/features/shared-module/quotas/services/quotas-logs.service';

@Component({
  selector: 'app-customer-quotas-logs-page',
  templateUrl: './customer-quotas-logs-page.component.html',
  styleUrl: './customer-quotas-logs-page.component.scss'
})
export class CustomerQuotasLogsPageComponent implements OnInit {
  logsCollection: IQuotasLogs[] = [];

  request: IGetQuotasLogsRequest = {
    typeId:undefined,
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    }
  }
  constructor(private quotasLogsService: QuotasLogsService,
    private toastr: MyToastrService) { }
  ngOnInit(): void {
    this.loadFirst();
  }

  isLoadMoreAvailable: boolean;
  isLoading: boolean;
  loadFirst() {
    if(this.isLoading)return;

    this.request.pageParameters.pageNumber=1;

    this.isLoading = true;
    this.quotasLogsService.getQuotasLogs(this.request).subscribe(res => {
      this.isLoading = false;
      this.logsCollection = res;

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    })
  }

  isLoading2: boolean;
  loadMore() {
    if(this.isLoading2)return;
    this.request.pageParameters.pageNumber++;

    this.isLoading2 = true;
    this.quotasLogsService.getQuotasLogs(this.request).subscribe(res => {
      this.isLoading2 = false;
      this.logsCollection = this.logsCollection.concat(res);

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading2 = false;
      this.toastr.error(error.error.Message);
    })
  }
  selectTypeId(typeId: string) {
    this.request.typeId=typeId;
    this.loadFirst();
  }
}
