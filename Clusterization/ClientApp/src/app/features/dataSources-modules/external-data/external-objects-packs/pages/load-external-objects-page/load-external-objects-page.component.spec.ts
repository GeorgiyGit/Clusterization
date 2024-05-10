import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadExternalObjectsPageComponent } from './load-external-objects-page.component';

describe('LoadExternalObjectsPageComponent', () => {
  let component: LoadExternalObjectsPageComponent;
  let fixture: ComponentFixture<LoadExternalObjectsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadExternalObjectsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoadExternalObjectsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
