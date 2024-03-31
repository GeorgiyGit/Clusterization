import { UserTasksService } from 'src/app/features/tasks/services/user-tasks.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IMyFullTask } from '../../models/my-full-task';

@Component({
  selector: 'app-full-task-page',
  templateUrl: './full-task-page.component.html',
  styleUrl: './full-task-page.component.scss',
  animations: [
    trigger('popUpAnimation', [
      state('in', style({ transform: 'translateY(0)' })),
      state('hidden', style({ transform: 'translateY(100%)' })),
      transition('void => in', [
        style({ transform: 'translateY(100%)' }),
        animate('300ms cubic-bezier(0.4, 0, 0.2, 1)')
      ]),
      transition('in => hidden', animate('300ms cubic-bezier(0.4, 0, 0.2, 1)'))
    ])
  ]
})
export class FullTaskPageComponent implements OnInit{
  animationState:string='in';


  fullTask:IMyFullTask;

  constructor(private router:Router,
    private toaster:MyToastrService,
    private route:ActivatedRoute,
    private userTasksService:UserTasksService){}
  ngOnInit(): void {
    this.animationState='in';

    let taskId=this.route.snapshot.params['id'];

    this.userTasksService.getFullTask(taskId).subscribe(res=>{
      this.fullTask=res;
      console.log(res);
    },error=>{
      this.toaster.error(error.error.Message);
    })
  }


  closeOverflow(){
    this.animationState='hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }
}
