import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocNavigationPageComponent } from './doc-navigation-page.component';

describe('DocNavigationPageComponent', () => {
  let component: DocNavigationPageComponent;
  let fixture: ComponentFixture<DocNavigationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocNavigationPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocNavigationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
