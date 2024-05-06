import { Component, OnInit } from '@angular/core';
import { IGetWorkspacesRequest } from '../../models/requests/getWorkspacesRequest';
import { ISimpleClusterizationWorkspace } from '../../models/responses/simpleClusterizationWorkspace';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IWorkspaceFilter } from '../../models/workspaceFilter';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

@Component({
  selector: 'app-workspace-list-page',
  templateUrl: './workspace-list-page.component.html',
  styleUrls: ['./workspace-list-page.component.scss']
})
export class WorkspaceListPageComponent implements OnInit {
  request: IGetWorkspacesRequest = {
    filterStr: '',
    typeId: undefined,
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    }
  }

  workspaces: ISimpleClusterizationWorkspace[] = [];

  constructor(private workspacesService: ClusterizationWorkspaceService,
    private toastr: MyToastrService,
    private router: Router,
    private accountService: AccountService) { }
  ngOnInit(): void {
    this.loadFirst();
  }


  changeFilter(filter: IWorkspaceFilter) {
    this.request.filterStr = filter.filterStr;
    this.request.typeId = filter.typeId;

    this.loadFirst();
  }

  isLoading: boolean;
  loadFirst() {
    if (this.isLoading) return;

    this.request.pageParameters.pageNumber = 0;

    this.isLoading = true;
    this.workspacesService.getWorkspaces(this.request).subscribe(res => {
      this.workspaces = res;
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
    this.workspacesService.getWorkspaces(this.request).subscribe(res => {
      this.workspaces = this.workspaces.concat(res);
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

  notAuthorizedErrorStr = $localize`Ви не авторизовані!`;
  visitorError = $localize`Недостатній рівень доступу. Для цієї дії необхідно підтвердити email!`;
  openAddWorkspace(event: MouseEvent) {
    if (!this.accountService.isAuthenticated()) {
      this.toastr.error(this.notAuthorizedErrorStr);
      return;
    }
    if (!this.accountService.isUserUser()) {
      this.toastr.error(this.visitorError);
      return;
    }
    
    this.router.navigate([{ outlets: { overflow: 'clusterization/workspaces/add' } }]);
  }
}
