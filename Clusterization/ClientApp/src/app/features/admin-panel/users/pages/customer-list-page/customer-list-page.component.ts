import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { YoutubeVideoService } from 'src/app/features/youtube/videos/services/youtube-video.service';
import { IGetCustomersRequest } from '../../models/requests/get-customers-request';
import { ISimpleCustomer } from '../../models/responses/simple-customer';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrl: './customer-list-page.component.scss'
})
export class CustomerListPageComponent implements OnInit{
  request:IGetCustomersRequest={
    filterStr:'',
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }
  customers:ISimpleCustomer[]=[];


  constructor(private user:YoutubeVideoService,
    private toastr:MyToastrService,
    private route:ActivatedRoute,
    private usersService:UsersService){}
  ngOnInit(): void {
    
    this.loadFirst();
  }


  changeFilterStr(str:string){
    this.request.filterStr=str;

    this.loadFirst();
  }

  isLoading:boolean;
  loadFirst(){
    this.request.pageParameters.pageNumber=1;

    this.isLoading=true;
    this.usersService.getCustomers(this.request).subscribe(res=>{
      this.customers=res;
      this.isLoading=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;
    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoading2:boolean;
  loadMore(){
    this.isLoading2=true;
    this.usersService.getCustomers(this.request).subscribe(res=>{
      this.customers=this.customers.concat(res);
      this.isLoading2=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;
    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoadMoreAvailable:boolean;
  addMore(){
    if(this.isLoadMoreAvailable){
      this.request.pageParameters.pageNumber++;
      this.loadMore();
    }
  }
}
