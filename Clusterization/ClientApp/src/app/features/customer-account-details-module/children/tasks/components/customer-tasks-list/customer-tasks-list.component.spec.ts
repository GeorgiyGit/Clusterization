import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerTasksListComponent } from './customer-tasks-list.component';

describe('CustomerTasksListComponent', () => {
  let component: CustomerTasksListComponent;
  let fixture: ComponentFixture<CustomerTasksListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerTasksListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerTasksListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
