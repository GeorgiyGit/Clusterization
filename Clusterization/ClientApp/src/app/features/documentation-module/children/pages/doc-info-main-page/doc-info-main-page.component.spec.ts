import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocInfoMainPageComponent } from './doc-info-main-page.component';

describe('DocInfoMainPageComponent', () => {
  let component: DocInfoMainPageComponent;
  let fixture: ComponentFixture<DocInfoMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocInfoMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocInfoMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
