import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalObjectsPackCardComponent } from './external-objects-pack-card.component';

describe('ExternalObjectsPackCardComponent', () => {
  let component: ExternalObjectsPackCardComponent;
  let fixture: ComponentFixture<ExternalObjectsPackCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalObjectsPackCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalObjectsPackCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
