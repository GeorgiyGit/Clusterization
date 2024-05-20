import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataObjectFullPageComponent } from './data-object-full-page.component';

describe('DataObjectFullPageComponent', () => {
  let component: DataObjectFullPageComponent;
  let fixture: ComponentFixture<DataObjectFullPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DataObjectFullPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DataObjectFullPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
