import { Component, OnInit } from '@angular/core';
import { IPageParameters } from 'src/app/core/models/page-parameters';
import { FastClusteringService } from '../../services/fast-clustering.service';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';
import { ISimpleClusterizationWorkspace } from '../../../workspace/models/responses/simpleClusterizationWorkspace';
import { IWorkspaceFilter } from '../../../workspace/models/workspaceFilter';

@Component({
  selector: 'app-fast-clustering-workspaces-list',
  templateUrl: './fast-clustering-workspaces-list.component.html',
  styleUrl: './fast-clustering-workspaces-list.component.scss'
})
export class FastClusteringWorkspacesListComponent implements OnInit {
  pageParameters:IPageParameters={
    pageNumber: 1,
    pageSize: 10
  }

  workspaces: ISimpleClusterizationWorkspace[] = [];

  constructor(private fastClusteringService: FastClusteringService,
    private toastr: MyToastrService,
    private router: Router,
    private accountService: AccountService) { }
  ngOnInit(): void {
    this.loadFirst();
  }

  isLoading: boolean;
  loadFirst() {
    if (this.isLoading) return;

    this.pageParameters.pageNumber = 1;

    this.isLoading = true;
    this.fastClusteringService.getWorkspaces(this.pageParameters).subscribe(res => {
      this.workspaces = res;
      this.isLoading = false;

      if (res.length < this.pageParameters.pageSize) this.isLoadMoreAvailable = false;
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
    this.fastClusteringService.getWorkspaces(this.pageParameters).subscribe(res => {
      this.workspaces = this.workspaces.concat(res);
      this.isLoading2 = false;

      if (res.length < this.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoadMoreAvailable: boolean;
  addMore() {
    if (this.isLoadMoreAvailable) {
      this.pageParameters.pageNumber++;
      this.loadMore();
    }
  }
}
