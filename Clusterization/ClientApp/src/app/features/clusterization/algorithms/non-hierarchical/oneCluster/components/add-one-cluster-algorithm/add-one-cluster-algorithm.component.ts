import { Component, OnInit } from '@angular/core';
import { IAddOneClusterAlgorithm } from '../../models/addOneClusterAlgorithm';
import { OneClusterAlgorithmService } from '../../services/one-cluster-algorithm.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-add-one-cluster-algorithm',
  templateUrl: './add-one-cluster-algorithm.component.html',
  styleUrls: ['./add-one-cluster-algorithm.component.scss']
})
export class AddOneClusterAlgorithmComponent implements OnInit {
  algorithmForm: FormGroup = this.fb.group({
    clusterColor: ["#000000", [Validators.required]]
  });

  get formValue() {
    return this.algorithmForm.value as IAddOneClusterAlgorithm;
  }

  get clusterColor() { return (this.algorithmForm.get('clusterColor')!); }

  constructor(private fb: FormBuilder,
    private oneClusterAlgorithmService: OneClusterAlgorithmService,
    private toastr: MyToastrService,
    private router:Router) { }
  ngOnInit(): void {
    this.algorithmForm = this.fb.group({
      clusterColor: [, [Validators.required]]
    });
  }

  isLoading: boolean;
  submit() {
    let model = this.formValue;

    this.oneClusterAlgorithmService.add(model).subscribe(res => {
      this.isLoading=false;
      this.toastr.success($localize`Алгоритм додано`);
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    })
  }
}
