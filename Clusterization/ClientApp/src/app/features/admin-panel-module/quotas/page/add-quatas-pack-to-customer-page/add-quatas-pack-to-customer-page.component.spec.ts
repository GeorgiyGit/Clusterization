import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddQuatasPackToCustomerPageComponent } from './add-quatas-pack-to-customer-page.component';

describe('AddQuatasPackToCustomerPageComponent', () => {
  let component: AddQuatasPackToCustomerPageComponent;
  let fixture: ComponentFixture<AddQuatasPackToCustomerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddQuatasPackToCustomerPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddQuatasPackToCustomerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
