import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateWorkspacePageComponent } from './update-workspace-page.component';

describe('UpdateWorkspacePageComponent', () => {
  let component: UpdateWorkspacePageComponent;
  let fixture: ComponentFixture<UpdateWorkspacePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateWorkspacePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateWorkspacePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
