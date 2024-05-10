import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalObjectsPackListComponent } from './external-objects-pack-list.component';

describe('ExternalObjectsPackListComponent', () => {
  let component: ExternalObjectsPackListComponent;
  let fixture: ComponentFixture<ExternalObjectsPackListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalObjectsPackListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalObjectsPackListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
