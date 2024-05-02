import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-workspaces-main-page',
  templateUrl: './customer-workspaces-main-page.component.html',
  styleUrl: './customer-workspaces-main-page.component.scss'
})
export class CustomerWorkspacesMainPageComponent {

  constructor(private router:Router){}
  openAddWorkspace(event:MouseEvent){
    this.router.navigate([{outlets: {overflow: 'clusterization/workspaces/add'}}]);
  }
}
