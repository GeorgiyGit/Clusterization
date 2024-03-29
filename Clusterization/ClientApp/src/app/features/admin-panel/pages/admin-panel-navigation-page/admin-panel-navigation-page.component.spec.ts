import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPanelNavigationPageComponent } from './admin-panel-navigation-page.component';

describe('AdminPanelNavigationPageComponent', () => {
  let component: AdminPanelNavigationPageComponent;
  let fixture: ComponentFixture<AdminPanelNavigationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminPanelNavigationPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminPanelNavigationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
