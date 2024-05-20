import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalDataDataObjectComponent } from './external-data-data-object.component';

describe('ExternalDataDataObjectComponent', () => {
  let component: ExternalDataDataObjectComponent;
  let fixture: ComponentFixture<ExternalDataDataObjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalDataDataObjectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalDataDataObjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
