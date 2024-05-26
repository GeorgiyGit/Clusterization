import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CircularProgressBarComponent } from '../features/shared-module/tasks/components/circular-progress-bar/circular-progress-bar.component';
import { ConfirmPageComponent } from './components/confirm-page/confirm-page.component';
import { LoaderComponent } from './components/loader/loader.component';
import { MoreActionSelectComponent } from './components/more-action-select/more-action-select.component';
import { SearchInputComponent } from './components/search-input/search-input.component';
import { SelectOptionInputComponent } from './components/select-option-input/select-option-input.component';
import { CloseOutsideDirective } from './directives/close-outside.directive';
import { FullNormalizedDateTimePipe } from './pipes/full-normalized-date-time.pipe';
import { NormalizedDateTimePipe } from './pipes/normalized-date-time.pipe';
import { TimeDifferencePipe } from './pipes/time-difference.pipe';
import { CustomerGuard } from './guard/customer.guard';
import { ModeratorGuard } from './guard/moderator.guard';
import { MatTooltipModule } from '@angular/material/tooltip';
import { LayoutModule } from '@angular/cdk/layout';
import { LongPressDirective } from './directives/long-press.directive';
import { SwipeableCardComponent } from './components/swipeable-card/swipeable-card.component';
import { TruncatePipe } from './pipes/truncate.pipe';

@NgModule({
  declarations: [
    SearchInputComponent,
    LongPressDirective,
    SelectOptionInputComponent,
    LoaderComponent,
    NormalizedDateTimePipe,
    MoreActionSelectComponent,
    CircularProgressBarComponent,
    FullNormalizedDateTimePipe,
    TimeDifferencePipe,
    ConfirmPageComponent,
    SwipeableCardComponent,
    TruncatePipe
  ],
  exports:[
    SearchInputComponent,
    LongPressDirective,
    SelectOptionInputComponent,
    LoaderComponent,
    NormalizedDateTimePipe,
    MoreActionSelectComponent,
    CircularProgressBarComponent,
    FullNormalizedDateTimePipe,
    TimeDifferencePipe,
    ConfirmPageComponent,
    SwipeableCardComponent,
    TruncatePipe
  ],
  imports: [
    CommonModule,
    MatTooltipModule,
    LayoutModule,
  ],
  providers:[
    CustomerGuard,
    ModeratorGuard
  ]
})
export class CoreModule { }
