import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateExternalObjectsPackPageComponent } from './update-external-objects-pack-page.component';

describe('UpdateExternalObjectsPackPageComponent', () => {
  let component: UpdateExternalObjectsPackPageComponent;
  let fixture: ComponentFixture<UpdateExternalObjectsPackPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateExternalObjectsPackPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateExternalObjectsPackPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
