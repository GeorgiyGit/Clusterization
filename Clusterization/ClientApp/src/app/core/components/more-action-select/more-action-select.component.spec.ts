import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoreActionSelectComponent } from './more-action-select.component';

describe('MoreActionSelectComponent', () => {
  let component: MoreActionSelectComponent;
  let fixture: ComponentFixture<MoreActionSelectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MoreActionSelectComponent]
    });
    fixture = TestBed.createComponent(MoreActionSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
