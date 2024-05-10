import { Component, Input } from '@angular/core';
import { ISimpleExternalObject } from '../../models/responses/simple-external-object';

@Component({
  selector: 'app-external-object-list',
  templateUrl: './external-object-list.component.html',
  styleUrl: './external-object-list.component.scss'
})
export class ExternalObjectListComponent {
@Input() externalObjects:ISimpleExternalObject[]
}
