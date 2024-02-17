import { Component,OnInit } from '@angular/core';
import { IAddGaussianMixtureAlgorithm } from '../../models/add-gaussian-mixture-algorithm';
import { GaussianMixtureService } from '../../services/gaussian-mixture.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-add-gaussian-mixture-algorithm',
  templateUrl: './add-gaussian-mixture-algorithm.component.html',
  styleUrls: ['./add-gaussian-mixture-algorithm.component.scss']
})
export class AddGaussianMixtureAlgorithmComponent implements OnInit {
  algorithmForm: FormGroup = this.fb.group({
    numberOfComponents: [0, [Validators.required, Validators.minLength(1)]],
  });

  get formValue() {
    return this.algorithmForm.value as IAddGaussianMixtureAlgorithm;
  }

  get numberOfComponents() { return (this.algorithmForm.get('numberOfComponents')!); }

  constructor(private fb: FormBuilder,
    private gaussianMixtureService: GaussianMixtureService,
    private toastr: MyToastrService,
    private router:Router) { }
  ngOnInit(): void {
  }

  isLoading: boolean;
  submit() {
    let model = this.formValue;

    this.gaussianMixtureService.add(model).subscribe(res => {
      this.isLoading=false;
      this.toastr.success($localize`Алгоритм додано`);
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    })
  }
}
