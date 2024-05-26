import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationProfileTasksListPageComponent } from './clusterization-profile-tasks-list-page.component';

describe('ClusterizationProfileTasksListPageComponent', () => {
  let component: ClusterizationProfileTasksListPageComponent;
  let fixture: ComponentFixture<ClusterizationProfileTasksListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClusterizationProfileTasksListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClusterizationProfileTasksListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
