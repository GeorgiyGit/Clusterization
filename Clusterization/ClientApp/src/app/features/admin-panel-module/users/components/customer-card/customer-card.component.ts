import { Component, Input, OnInit } from '@angular/core';
import { ISimpleCustomer } from '../../models/responses/simple-customer';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Clipboard } from '@angular/cdk/clipboard';
import { ISelectAction } from 'src/app/core/models/select-action';
import { RolesService } from '../../services/roles.service';

@Component({
  selector: 'app-customer-card',
  templateUrl: './customer-card.component.html',
  styleUrl: './customer-card.component.scss'
})
export class CustomerCardComponent implements OnInit {
  @Input() customer: ISimpleCustomer;

  actions: ISelectAction[] = [];

  constructor(private clipboard: Clipboard,
    private toastr: ToastrService,
    private router: Router,
    private rolesService: RolesService,
    private route:ActivatedRoute) { }

  ngOnInit(): void {
    if (this.customer == null) return;
    this.actions = [];
    this.addActions();
  }

  addActions() {
    if (this.customer.isModerator) {
      this.actions.push({
        name: $localize`Видалити з модераторів`,
        action: () => {
          this.rolesService.removeModerator(this.customer.id).subscribe(res => {
            this.customer.isModerator = false;

            this.actions = [];
            this.addActions();
          }, error => {
            this.toastr.error(error.error.Message);
          });
        },
        isForAuthorized: true,
        isOnlyForUsers: true
      });
    }
    else {
      this.actions.push({
        name: $localize`Додати до модераторів`,
        action: () => {
          this.rolesService.addModerator(this.customer.id).subscribe(res => {
            this.customer.isModerator = true;

            this.actions = [];
            this.addActions();
          }, error => {
            this.toastr.error(error.error.Message);
          });
        },
        isForAuthorized: true,
        isOnlyForUsers: true
      });
    }
    this.actions.push({
      name: $localize`Додати пак квот`,
      action: () => {
        this.router.navigate([{ outlets: { overflow: 'admin-panel/add-quotas-pack/' + this.customer.id } }]);
      },
      isForAuthorized: true,
      isOnlyForUsers: true
    });

    this.actions.push({
      name: $localize`Список завдань`,
      action: () => {
        this.router.navigate(['../tasks', this.customer.id], { relativeTo: this.route });
      },
      isForAuthorized: true,
      isOnlyForUsers: true
    });
  }

  copyToClipboard(text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success($localize`Скопійовано!!!`);
  }
}
