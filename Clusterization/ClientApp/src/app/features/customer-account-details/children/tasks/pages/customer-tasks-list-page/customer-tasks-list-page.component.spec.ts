import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerTasksListPageComponent } from './customer-tasks-list-page.component';

describe('CustomerTasksListPageComponent', () => {
  let component: CustomerTasksListPageComponent;
  let fixture: ComponentFixture<CustomerTasksListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerTasksListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerTasksListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
