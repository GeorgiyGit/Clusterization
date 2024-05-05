import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTelegramMessageRepliesToWorkspaceComponent } from './add-telegram-message-replies-to-workspace.component';

describe('AddTelegramMessageRepliesToWorkspaceComponent', () => {
  let component: AddTelegramMessageRepliesToWorkspaceComponent;
  let fixture: ComponentFixture<AddTelegramMessageRepliesToWorkspaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddTelegramMessageRepliesToWorkspaceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddTelegramMessageRepliesToWorkspaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
