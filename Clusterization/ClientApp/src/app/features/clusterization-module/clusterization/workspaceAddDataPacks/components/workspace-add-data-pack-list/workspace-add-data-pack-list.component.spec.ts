import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspaceAddDataPackListComponent } from './workspace-add-data-pack-list.component';

describe('WorkspaceAddDataPackListComponent', () => {
  let component: WorkspaceAddDataPackListComponent;
  let fixture: ComponentFixture<WorkspaceAddDataPackListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkspaceAddDataPackListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkspaceAddDataPackListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
