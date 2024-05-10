import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadAndAddExternalObjectsPageComponent } from './load-and-add-external-objects-page.component';

describe('LoadAndAddExternalObjectsPageComponent', () => {
  let component: LoadAndAddExternalObjectsPageComponent;
  let fixture: ComponentFixture<LoadAndAddExternalObjectsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadAndAddExternalObjectsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoadAndAddExternalObjectsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
