import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDbscanAlgorithmComponent } from './add-dbscan-algorithm.component';

describe('AddDbscanAlgorithmComponent', () => {
  let component: AddDbscanAlgorithmComponent;
  let fixture: ComponentFixture<AddDbscanAlgorithmComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddDbscanAlgorithmComponent]
    });
    fixture = TestBed.createComponent(AddDbscanAlgorithmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
