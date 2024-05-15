import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerEdPacksLoadedListPageComponent } from './customer-ed-packs-loaded-list-page.component';

describe('CustomerEdPacksLoadedListPageComponent', () => {
  let component: CustomerEdPacksLoadedListPageComponent;
  let fixture: ComponentFixture<CustomerEdPacksLoadedListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerEdPacksLoadedListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerEdPacksLoadedListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
