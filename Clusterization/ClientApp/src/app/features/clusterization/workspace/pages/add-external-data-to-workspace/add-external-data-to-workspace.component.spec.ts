import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExternalDataToWorkspaceComponent } from './add-external-data-to-workspace.component';

describe('AddExternalDataToWorkspaceComponent', () => {
  let component: AddExternalDataToWorkspaceComponent;
  let fixture: ComponentFixture<AddExternalDataToWorkspaceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddExternalDataToWorkspaceComponent]
    });
    fixture = TestBed.createComponent(AddExternalDataToWorkspaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
