import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerWorkspacesMainPageComponent } from './customer-workspaces-main-page.component';

describe('CustomerWorkspacesMainPageComponent', () => {
  let component: CustomerWorkspacesMainPageComponent;
  let fixture: ComponentFixture<CustomerWorkspacesMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerWorkspacesMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerWorkspacesMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
