import { AfterViewInit, Component } from '@angular/core';
import { IFullFastClustering } from '../../models/requests/full-fast-clustering-request';
import { FastClusteringService } from '../../services/fast-clustering.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-fast-clustering-full',
  templateUrl: './add-fast-clustering-full.component.html',
  styleUrl: './add-fast-clustering-full.component.scss'
})
export class AddFastClusteringFullComponent implements AfterViewInit {
  algorithmTypeId: string = 'DBSCAN';
  algorithmId: number = 1;

  dimensionTypeId: number = 2;

  DRTechniqueId: string = 't-SNE';

  embeddingModelId: string = 'text-embedding-3-small';

  isAlgorithmsSelectActive: boolean = true;

  isMoreOptionsOpen: boolean;

  fastClusteringForm: FormGroup = this.fb.group({
    title: ['', [Validators.maxLength(100)]],
    description: ['', [Validators.maxLength(3000)]],
    text: ['', [Validators.required, Validators.maxLength(30000)]],
  });

  get formValue() {
    return this.fastClusteringForm.value as IFullFastClusteringForm;
  }

  get text() { return (this.fastClusteringForm.get('text')!); }
  get title() { return (this.fastClusteringForm.get('title')!); }
  get description() { return this.fastClusteringForm.get('description')!; }

  constructor(private fb: FormBuilder,
    private fastClusteringService: FastClusteringService,
    private toaster: MyToastrService,
    private router: Router,
    private route: ActivatedRoute) { }
  ngAfterViewInit(): void {
    this.isAlgorithmsSelectActive = true;
  }

  isLoading: boolean;
  submit() {
    if (this.algorithmId == undefined) {
      this.toaster.error($localize`Тип не вибраний`);
      return;
    }

    if (this.dimensionTypeId == undefined) {
      this.toaster.error($localize`Розмірність не вибрана`);
      return;
    }

    let formModel = this.formValue;

    let model:IFullFastClustering={
      title:formModel.title,
      description:formModel.description,
      algorithmId:this.algorithmId,
      dimensionCount:this.dimensionTypeId,
      drTechniqueId:this.DRTechniqueId,
      embeddingModelId:this.embeddingModelId,
      texts:formModel.text.split('\n')
    };

    this.isLoading = true;
    this.fastClusteringService.fullFastClustering(model).subscribe(res => {
      this.toaster.success($localize`Завдання створено`);
      this.isLoading = false;

      this.router.navigateByUrl('main-layout/clusterization/profiles/full/' + res);
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });
  }

  changeAlgorithmType(id: string) {
    if (id == undefined) this.isAlgorithmsSelectActive = false;
    else this.isAlgorithmsSelectActive = true;
    this.algorithmTypeId = id;
  }

  changeAlgorithm(id: string) {
    this.algorithmId = parseInt(id);
  }

  changeDimensionType(id: number) {
    this.dimensionTypeId = id;
  }
  changeDRTechniqueId(id: string) {
    this.DRTechniqueId = id;
  }

  changeEmbeddingModel(id: string) {
    this.embeddingModelId = id;
  }

  toggleMoreOptions() {
    this.isMoreOptionsOpen = !this.isMoreOptionsOpen;
  }
}
export interface IFullFastClusteringForm{
  text:string,
  title:string,
  description:string
}
