import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFastClusteringWorkspaceComponent } from './add-fast-clustering-workspace.component';

describe('AddFastClusteringWorkspaceComponent', () => {
  let component: AddFastClusteringWorkspaceComponent;
  let fixture: ComponentFixture<AddFastClusteringWorkspaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddFastClusteringWorkspaceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddFastClusteringWorkspaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
