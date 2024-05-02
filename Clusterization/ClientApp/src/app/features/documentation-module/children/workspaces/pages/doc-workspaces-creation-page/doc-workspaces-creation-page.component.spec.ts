import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocWorkspacesCreationPageComponent } from './doc-workspaces-creation-page.component';

describe('DocWorkspacesCreationPageComponent', () => {
  let component: DocWorkspacesCreationPageComponent;
  let fixture: ComponentFixture<DocWorkspacesCreationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocWorkspacesCreationPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocWorkspacesCreationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
