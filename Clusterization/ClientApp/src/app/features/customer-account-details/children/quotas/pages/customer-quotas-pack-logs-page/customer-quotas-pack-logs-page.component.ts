import { Component, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetQuotasPackLogsRequest } from 'src/app/features/quotas/models/requests/get-quotas-pack-logs-request';
import { IQuotasPackLogs } from 'src/app/features/quotas/models/responses/quotas-pack-logs';
import { QuotasLogsService } from 'src/app/features/quotas/services/quotas-logs.service';

@Component({
  selector: 'app-customer-quotas-pack-logs-page',
  templateUrl: './customer-quotas-pack-logs-page.component.html',
  styleUrl: './customer-quotas-pack-logs-page.component.scss'
})
export class CustomerQuotasPackLogsPageComponent implements OnInit {
  logsCollection: IQuotasPackLogs[] = [];

  request: IGetQuotasPackLogsRequest = {
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
    this.request.pageParameters.pageNumber=1;

    this.isLoading = true;
    this.quotasLogsService.getQuotasPackLogs(this.request).subscribe(res => {
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
    this.request.pageParameters.pageNumber++;

    this.isLoading2 = true;
    this.quotasLogsService.getQuotasPackLogs(this.request).subscribe(res => {
      this.isLoading = false;
      this.logsCollection = this.logsCollection.concat(res);

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    })
  }
}
