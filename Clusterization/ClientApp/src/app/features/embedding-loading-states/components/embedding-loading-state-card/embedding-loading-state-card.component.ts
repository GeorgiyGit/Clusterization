import { Component, Input } from '@angular/core';
import { IEmbeddingLoadingState } from '../../models/responses/embedding-loading-state';

@Component({
  selector: 'app-embedding-loading-state-card',
  templateUrl: './embedding-loading-state-card.component.html',
  styleUrl: './embedding-loading-state-card.component.scss'
})
export class EmbeddingLoadingStateCardComponent {
  @Input() state:IEmbeddingLoadingState;
}
