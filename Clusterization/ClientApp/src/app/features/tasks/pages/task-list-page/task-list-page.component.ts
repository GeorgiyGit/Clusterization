import { Component, OnInit } from '@angular/core';
import { MyTaskService } from '../../services/my-task.service';
import { IMyTask } from '../../models/myTask';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-task-list-page',
  templateUrl: './task-list-page.component.html',
  styleUrls: ['./task-list-page.component.scss']
})
export class TaskListPageComponent implements OnInit{
  tasks:IMyTask[]=[];

  constructor(private myTaskService:MyTaskService,
    private toastr:MyToastrService){}

  isLoading:boolean;
  ngOnInit(): void {
    this.load();
  }

  load(){
    this.isLoading=true;
    this.myTaskService.getAll().subscribe(res=>{
      this.tasks=res;
      this.isLoading=false;
      console.log(this.tasks);
    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    });
  }

}
