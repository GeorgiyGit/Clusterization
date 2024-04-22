import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleWorkspaceAddDataPackCardComponent } from './simple-workspace-add-data-pack-card.component';

describe('SimpleWorkspaceAddDataPackCardComponent', () => {
  let component: SimpleWorkspaceAddDataPackCardComponent;
  let fixture: ComponentFixture<SimpleWorkspaceAddDataPackCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SimpleWorkspaceAddDataPackCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SimpleWorkspaceAddDataPackCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
