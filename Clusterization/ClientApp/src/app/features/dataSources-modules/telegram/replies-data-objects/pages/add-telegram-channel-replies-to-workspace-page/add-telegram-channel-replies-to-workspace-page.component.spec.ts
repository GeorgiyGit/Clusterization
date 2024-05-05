import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTelegramChannelRepliesToWorkspacePageComponent } from './add-telegram-channel-replies-to-workspace-page.component';

describe('AddTelegramChannelRepliesToWorkspacePageComponent', () => {
  let component: AddTelegramChannelRepliesToWorkspacePageComponent;
  let fixture: ComponentFixture<AddTelegramChannelRepliesToWorkspacePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddTelegramChannelRepliesToWorkspacePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddTelegramChannelRepliesToWorkspacePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
