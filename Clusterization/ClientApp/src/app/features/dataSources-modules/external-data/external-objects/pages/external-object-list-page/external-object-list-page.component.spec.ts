import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalObjectListPageComponent } from './external-object-list-page.component';

describe('ExternalObjectListPageComponent', () => {
  let component: ExternalObjectListPageComponent;
  let fixture: ComponentFixture<ExternalObjectListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalObjectListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalObjectListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
