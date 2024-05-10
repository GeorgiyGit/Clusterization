import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalObjectsPackListPageComponent } from './external-objects-pack-list-page.component';

describe('ExternalObjectsPackListPageComponent', () => {
  let component: ExternalObjectsPackListPageComponent;
  let fixture: ComponentFixture<ExternalObjectsPackListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalObjectsPackListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalObjectsPackListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
