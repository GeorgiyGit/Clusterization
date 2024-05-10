import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalDataHeaderComponent } from './external-data-header.component';

describe('ExternalDataHeaderComponent', () => {
  let component: ExternalDataHeaderComponent;
  let fixture: ComponentFixture<ExternalDataHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalDataHeaderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalDataHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
