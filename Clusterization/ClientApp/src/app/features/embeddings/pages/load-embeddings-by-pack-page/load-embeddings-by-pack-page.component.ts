import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { EmbeddingsLoadingService } from '../../services/embeddings-loading.service';

@Component({
  selector: 'app-load-embeddings-by-pack-page',
  templateUrl: './load-embeddings-by-pack-page.component.html',
  styleUrl: './load-embeddings-by-pack-page.component.scss',
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
export class LoadEmbeddingsByPackPageComponent implements OnInit {
  animationState: string = 'in';

  embeddingModelId: string;
  packId:number;

  constructor(private embeddingsLoadingService:EmbeddingsLoadingService,
    private toaster: MyToastrService,
    private router: Router,
    private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.animationState = 'in';

    this.packId = this.route.snapshot.params['id'];
  }

  isLoading: boolean;
  submit() {
    this.embeddingsLoadingService.loadEmbeddingsByWorkspaceDataPack(this.packId,this.embeddingModelId).subscribe(res=>{
      this.toaster.success($localize`Завдання успішно створене`);
      this.closeOverflow();
    },error=>{
      this.toaster.error(error.error.Message);
    })
  }

  selectEmbeddingModelId(id:string){
    this.embeddingModelId=id;
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }
}
