import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocWorkspacesAddingDataPageComponent } from './doc-workspaces-adding-data-page.component';

describe('DocWorkspacesAddingDataPageComponent', () => {
  let component: DocWorkspacesAddingDataPageComponent;
  let fixture: ComponentFixture<DocWorkspacesAddingDataPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocWorkspacesAddingDataPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocWorkspacesAddingDataPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
