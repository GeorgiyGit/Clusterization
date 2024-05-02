import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspaceAddPackFullPageComponent } from './workspace-add-pack-full-page.component';

describe('WorkspaceAddPackFullPageComponent', () => {
  let component: WorkspaceAddPackFullPageComponent;
  let fixture: ComponentFixture<WorkspaceAddPackFullPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkspaceAddPackFullPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkspaceAddPackFullPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
