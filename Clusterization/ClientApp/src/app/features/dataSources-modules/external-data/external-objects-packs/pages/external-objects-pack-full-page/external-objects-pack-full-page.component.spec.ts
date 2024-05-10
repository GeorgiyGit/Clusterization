import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalObjectsPackFullPageComponent } from './external-objects-pack-full-page.component';

describe('ExternalObjectsPackFullPageComponent', () => {
  let component: ExternalObjectsPackFullPageComponent;
  let fixture: ComponentFixture<ExternalObjectsPackFullPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalObjectsPackFullPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalObjectsPackFullPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
