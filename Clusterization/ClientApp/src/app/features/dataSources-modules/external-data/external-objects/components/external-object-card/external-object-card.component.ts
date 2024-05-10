import { Component, Input } from '@angular/core';
import { ISimpleExternalObject } from '../../models/responses/simple-external-object';

@Component({
  selector: 'app-external-object-card',
  templateUrl: './external-object-card.component.html',
  styleUrl: './external-object-card.component.scss'
})
export class ExternalObjectCardComponent {
  @Input() externalObject: ISimpleExternalObject;
}
