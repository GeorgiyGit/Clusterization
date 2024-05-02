import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddScAlgorithmComponent } from './add-sc-algorithm.component';

describe('AddScAlgorithmComponent', () => {
  let component: AddScAlgorithmComponent;
  let fixture: ComponentFixture<AddScAlgorithmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddScAlgorithmComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddScAlgorithmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
