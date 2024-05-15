import { Component, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetExternalObjectsPacksRequest } from 'src/app/features/dataSources-modules/external-data/external-objects-packs/models/requests/get-external-objects-packs-request';
import { ISimpleExternalObjectsPack } from 'src/app/features/dataSources-modules/external-data/external-objects-packs/models/responses/simple-external-objects-pack';
import { ExternalObjectsPacksService } from 'src/app/features/dataSources-modules/external-data/external-objects-packs/services/external-objects-packs.service';

@Component({
  selector: 'app-customer-external-data-packs-loaded-list-page',
  templateUrl: './customer-ed-packs-loaded-list-page.component.html',
  styleUrl: './customer-ed-packs-loaded-list-page.component.scss'
})
export class CustomerEdPacksLoadedListPageComponent  implements OnInit {
  request: IGetExternalObjectsPacksRequest = {
    filterStr: '',
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    }
  }

  packs: ISimpleExternalObjectsPack[] = [];

  constructor(private packsService: ExternalObjectsPacksService,
    private toastr: MyToastrService) { }
  ngOnInit(): void {
    this.loadFirst();
  }


  searchStrChanges(str:string) {
    this.request.filterStr = str;

    this.loadFirst();
  }

  isLoading: boolean;
  loadFirst() {
    if (this.isLoading) return;

    this.request.pageParameters.pageNumber = 1;

    this.isLoading = true;
    this.packsService.getCustomerCollection(this.request).subscribe(res => {
      this.packs = res;
      this.isLoading = false;

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoading2: boolean;
  loadMore() {
    if (this.isLoading2) return;
    this.isLoading2 = true;
    this.packsService.getCustomerCollection(this.request).subscribe(res => {
      this.packs = this.packs.concat(res);
      this.isLoading2 = false;

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoadMoreAvailable: boolean;
  addMore() {
    if (this.isLoadMoreAvailable) {
      this.request.pageParameters.pageNumber++;
      this.loadMore();
    }
  }
}
