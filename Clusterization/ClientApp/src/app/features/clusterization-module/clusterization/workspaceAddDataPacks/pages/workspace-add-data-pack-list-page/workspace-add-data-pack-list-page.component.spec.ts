import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspaceAddDataPackListPageComponent } from './workspace-add-data-pack-list-page.component';

describe('WorkspaceAddDataPackListPageComponent', () => {
  let component: WorkspaceAddDataPackListPageComponent;
  let fixture: ComponentFixture<WorkspaceAddDataPackListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkspaceAddDataPackListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkspaceAddDataPackListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
