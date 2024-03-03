import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocWorkspacesDisplayingPageComponent } from './doc-workspaces-displaying-page.component';

describe('DocWorkspacesDisplayingPageComponent', () => {
  let component: DocWorkspacesDisplayingPageComponent;
  let fixture: ComponentFixture<DocWorkspacesDisplayingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocWorkspacesDisplayingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocWorkspacesDisplayingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
