import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalObjectListComponent } from './external-object-list.component';

describe('ExternalObjectListComponent', () => {
  let component: ExternalObjectListComponent;
  let fixture: ComponentFixture<ExternalObjectListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalObjectListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalObjectListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
