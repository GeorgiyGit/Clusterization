import { Component, OnInit } from '@angular/core';
import { IClusterizationWorkspace } from '../../models/clusterizationWorkspace';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-workspace-full-page',
  templateUrl: './workspace-full-page.component.html',
  styleUrls: ['./workspace-full-page.component.scss']
})
export class WorkspaceFullPageComponent implements OnInit {
  workspace: IClusterizationWorkspace;


  isLoading: boolean;
  constructor(private workspaceService: ClusterizationWorkspaceService,
    private route: ActivatedRoute,
    private router:Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.workspaceService.getById(id).subscribe(res => {
      this.workspace = res;
      
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  copyToClipboard(msg: string, text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success(msg + ' ' + 'скопійовано!!!');
  }
}
