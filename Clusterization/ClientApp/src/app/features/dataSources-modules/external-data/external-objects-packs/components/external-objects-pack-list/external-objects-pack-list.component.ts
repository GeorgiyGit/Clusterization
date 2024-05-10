import { Component, Input } from '@angular/core';
import { ISimpleExternalObjectsPack } from '../../models/responses/simple-external-objects-pack';

@Component({
  selector: 'app-external-objects-pack-list',
  templateUrl: './external-objects-pack-list.component.html',
  styleUrl: './external-objects-pack-list.component.scss'
})
export class ExternalObjectsPackListComponent {
  @Input() packs:ISimpleExternalObjectsPack[];
}
