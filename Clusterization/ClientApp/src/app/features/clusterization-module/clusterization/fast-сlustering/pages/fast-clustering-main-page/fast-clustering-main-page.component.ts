import { Component, OnInit } from '@angular/core';
import { FastClusteringService } from '../../services/fast-clustering.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-fast-clustering-main-page',
  templateUrl: './fast-clustering-main-page.component.html',
  styleUrl: './fast-clustering-main-page.component.scss'
})
export class FastClusteringMainPageComponent implements OnInit{

  constructor(private fastClusteringService:FastClusteringService,
    private toastr:MyToastrService
  ){}
  ngOnInit(): void {
    this.fastClusteringService.createWorkflow().subscribe(res=>{

    },error=>{
      this.toastr.error(error.error.Message);
    })
  }
}
