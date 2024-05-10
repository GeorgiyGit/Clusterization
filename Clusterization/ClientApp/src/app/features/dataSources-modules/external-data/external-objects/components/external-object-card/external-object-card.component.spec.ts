import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalObjectCardComponent } from './external-object-card.component';

describe('ExternalObjectCardComponent', () => {
  let component: ExternalObjectCardComponent;
  let fixture: ComponentFixture<ExternalObjectCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalObjectCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalObjectCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
