import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IAddKMeansAlgorithm } from '../../../k-means/models/addKMeansAlgorithm';
import { KMeansService } from '../../../k-means/services/kmeans.service';
import { IAddDBSCANAlgorithm } from '../../models/add-dbscan-algorithm';
import { DbscanService } from '../../services/dbscan.service';

@Component({
  selector: 'app-add-dbscan-algorithm',
  templateUrl: './add-dbscan-algorithm.component.html',
  styleUrls: ['./add-dbscan-algorithm.component.scss']
})
export class AddDbscanAlgorithmComponent implements OnInit {
  algorithmForm: FormGroup = this.fb.group({
    epsilon: [1, [Validators.required,Validators.minLength(0),Validators.maxLength(100)]],
    minimumPointsPerCluster: [1, [Validators.required,Validators.minLength(1)]]
  });

  get formValue() {
    return this.algorithmForm.value as IAddDBSCANAlgorithm;
  }

  get epsilon() { return (this.algorithmForm.get('epsilon')!); }
  get minimumPointsPerCluster() { return this.algorithmForm.get('minimumPointsPerCluster')!; }

  constructor(private fb: FormBuilder,
    private dbScanService: DbscanService,
    private toastr: MyToastrService,
    private router:Router) { }
  ngOnInit(): void {
  }

  isLoading: boolean;
  submit() {
    let model = this.formValue;

    this.dbScanService.add(model).subscribe(res => {
      this.isLoading=false;
      this.toastr.success('Алгоритм додано');
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    })
  }
}
