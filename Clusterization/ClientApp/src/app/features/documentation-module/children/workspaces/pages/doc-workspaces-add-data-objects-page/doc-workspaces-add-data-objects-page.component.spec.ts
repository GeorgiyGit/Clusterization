import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocWorkspacesAddDataObjectsPageComponent } from './doc-workspaces-add-data-objects-page.component';

describe('DocWorkspacesAddDataObjectsPageComponent', () => {
  let component: DocWorkspacesAddDataObjectsPageComponent;
  let fixture: ComponentFixture<DocWorkspacesAddDataObjectsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocWorkspacesAddDataObjectsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocWorkspacesAddDataObjectsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
