import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IAddKMeansAlgorithm } from '../../models/addKMeansAlgorithm';
import { KMeansService } from '../../services/kmeans.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-k-means-algorithm',
  templateUrl: './add-k-means-algorithm.component.html',
  styleUrls: ['./add-k-means-algorithm.component.scss']
})
export class AddKMeansAlgorithmComponent implements OnInit {
  algorithmForm: FormGroup = this.fb.group({
    numClusters: [0, [Validators.required, Validators.minLength(1)]],
    seed: [0, [Validators.required]]
  });

  get formValue() {
    return this.algorithmForm.value as IAddKMeansAlgorithm;
  }

  get numClusters() { return (this.algorithmForm.get('numClusters')!); }
  get seed() { return this.algorithmForm.get('seed')!; }

  constructor(private fb: FormBuilder,
    private kMeansService: KMeansService,
    private toastr: MyToastrService,
    private router:Router) { }
  ngOnInit(): void {
    this.algorithmForm = this.fb.group({
      numClusters: [, [Validators.required, Validators.minLength(1)]],
      seed: [Math.floor(Math.random() * 101), [Validators.required]]
    });
  }

  isLoading: boolean;
  submit() {
    let model = this.formValue;

    this.kMeansService.add(model).subscribe(res => {
      this.isLoading=false;
      this.toastr.success($localize`Алгоритм додано`);
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    })
  }
}
