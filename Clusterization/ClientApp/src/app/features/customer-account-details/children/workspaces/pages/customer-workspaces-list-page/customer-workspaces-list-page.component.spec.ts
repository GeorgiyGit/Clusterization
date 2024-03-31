import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerWorkspacesListPageComponent } from './customer-workspaces-list-page.component';

describe('CustomerWorkspacesListPageComponent', () => {
  let component: CustomerWorkspacesListPageComponent;
  let fixture: ComponentFixture<CustomerWorkspacesListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerWorkspacesListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerWorkspacesListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
