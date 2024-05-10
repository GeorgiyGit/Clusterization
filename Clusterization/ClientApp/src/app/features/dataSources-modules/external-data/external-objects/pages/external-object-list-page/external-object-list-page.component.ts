import { Component, OnInit } from '@angular/core';
import { IGetExternalObjectsRequest } from '../../models/requests/get-external-objects-request';
import { ISimpleExternalObject } from '../../models/responses/simple-external-object';
import { ExternalObjectsService } from '../../services/external-objects.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ActivatedRoute, Route } from '@angular/router';

@Component({
  selector: 'app-external-object-list-page',
  templateUrl: './external-object-list-page.component.html',
  styleUrl: './external-object-list-page.component.scss'
})
export class ExternalObjectListPageComponent implements OnInit {
  request: IGetExternalObjectsRequest = {
    packId:-1,
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    }
  }

  objects: ISimpleExternalObject[] = [];

  constructor(private externalObjectsService: ExternalObjectsService,
    private toastr: MyToastrService,
    private route:ActivatedRoute) { }
  ngOnInit(): void {
    this.request.packId = this.route.snapshot.params['id'];
    this.loadFirst();
  }

  isLoading: boolean;
  loadFirst() {
    if (this.isLoading) return;

    this.request.pageParameters.pageNumber = 1;

    this.isLoading = true;
    this.externalObjectsService.getCollection(this.request).subscribe(res => {
      this.objects = res;
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
    this.externalObjectsService.getCollection(this.request).subscribe(res => {
      this.objects = this.objects.concat(res);
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
