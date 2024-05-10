import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalDataLayoutComponent } from './external-data-layout.component';

describe('ExternalDataLayoutComponent', () => {
  let component: ExternalDataLayoutComponent;
  let fixture: ComponentFixture<ExternalDataLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalDataLayoutComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalDataLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
