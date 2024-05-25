import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FastClusteringWorkspacesListComponent } from './fast-clustering-workspaces-list.component';

describe('FastClusteringWorkspacesListComponent', () => {
  let component: FastClusteringWorkspacesListComponent;
  let fixture: ComponentFixture<FastClusteringWorkspacesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FastClusteringWorkspacesListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FastClusteringWorkspacesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
