import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocWorkspacesMainPageComponent } from './doc-workspaces-main-page.component';

describe('DocWorkspacesMainPageComponent', () => {
  let component: DocWorkspacesMainPageComponent;
  let fixture: ComponentFixture<DocWorkspacesMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocWorkspacesMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocWorkspacesMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
