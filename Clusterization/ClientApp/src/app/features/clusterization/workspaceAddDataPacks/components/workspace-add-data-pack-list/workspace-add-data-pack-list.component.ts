import { Component, Input } from '@angular/core';
import { ISimpleWorkspaceAddDataPack } from '../../models/responses/simple-workspace-add-data-pack';

@Component({
  selector: 'app-workspace-add-data-pack-list',
  templateUrl: './workspace-add-data-pack-list.component.html',
  styleUrl: './workspace-add-data-pack-list.component.scss'
})
export class WorkspaceAddDataPackListComponent {
  @Input() packs: ISimpleWorkspaceAddDataPack[] = [];
}
