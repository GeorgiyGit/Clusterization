import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IAddQuotasToCustomer } from 'src/app/features/shared-module/quotas/models/requests/add-quotas-to-customer';
import { IQuotasPack } from 'src/app/features/shared-module/quotas/models/responses/quotas-pack';
import { CustomerQuotasService } from 'src/app/features/shared-module/quotas/services/customer-quotas.service';

@Component({
  selector: 'app-add-quatas-pack-to-customer-page',
  templateUrl: './add-quatas-pack-to-customer-page.component.html',
  styleUrl: './add-quatas-pack-to-customer-page.component.scss',
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
export class AddQuatasPackToCustomerPageComponent implements OnInit {
  animationState: string = 'in';
  customerId: string;

  pack: IQuotasPack;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private customerQuotasService: CustomerQuotasService,
    private toaster: MyToastrService) { }
  ngOnInit(): void {
    this.animationState = 'in';

    this.customerId = this.route.snapshot.params['customerId'];
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

  isVideoDateCount: boolean = true;

  isLoading: boolean;
  add() {
    if (this.pack == null) {
      this.toaster.error($localize`Пак не вибрано!!!`);
    }

    let request: IAddQuotasToCustomer = {
      packId: this.pack.id,
      customerId: this.customerId
    };

    this.customerQuotasService.addQuotasToCustomer(request).subscribe(res => {
      this.toaster.success($localize`Пак додано`);
      this.isLoading = false;
      this.closeOverflow();
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });
  }


  selectPack(pack: IQuotasPack) {
    this.pack = pack;
  }
}
