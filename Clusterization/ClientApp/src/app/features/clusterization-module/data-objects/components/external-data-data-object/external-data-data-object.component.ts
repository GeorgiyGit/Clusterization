import { Component, Input } from '@angular/core';
import { ISimpleExternalObject } from 'src/app/features/dataSources-modules/external-data/external-objects/models/responses/simple-external-object';

@Component({
  selector: 'app-external-data-data-object',
  templateUrl: './external-data-data-object.component.html',
  styleUrl: './external-data-data-object.component.scss'
})
export class ExternalDataDataObjectComponent {
  @Input() model: ISimpleExternalObject;
}
