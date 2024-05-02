import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IAddSpectralClusteringAlgorithm } from '../../models/add-spectral-clustering-algorithm';
import { SpectralClusteringService } from '../../services/spectral-clustering.service';

@Component({
  selector: 'app-add-spectral-clustering-algorithm',
  templateUrl: './add-sc-algorithm.component.html',
  styleUrl: './add-sc-algorithm.component.scss'
})
export class AddScAlgorithmComponent implements OnInit {
  algorithmForm: FormGroup = this.fb.group({
    numClusters: [1, [Validators.required, Validators.minLength(1)]],
    gamma: [0, [Validators.required]]
  });

  get formValue() {
    return this.algorithmForm.value as IAddSpectralClusteringAlgorithm;
  }

  get numClusters() { return (this.algorithmForm.get('numClusters')!); }
  get gamma() { return this.algorithmForm.get('gamma')!; }

  constructor(private fb: FormBuilder,
    private spectralClusteringService: SpectralClusteringService,
    private toastr: MyToastrService,
    private router:Router) { }
  ngOnInit(): void {
  }

  isLoading: boolean;
  submit() {
    let model = this.formValue;

    this.spectralClusteringService.add(model).subscribe(res => {
      this.isLoading=false;
      this.toastr.success($localize`Алгоритм додано`);
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    })
  }
}
