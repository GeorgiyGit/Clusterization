import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWorkspacePageComponent } from './add-workspace-page.component';

describe('AddWorkspacePageComponent', () => {
  let component: AddWorkspacePageComponent;
  let fixture: ComponentFixture<AddWorkspacePageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddWorkspacePageComponent]
    });
    fixture = TestBed.createComponent(AddWorkspacePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
