import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DiffComponent } from './diff/diff.component';
import { CountDaysWeekComponent } from './count-days-week/count-days-week.component';
import { HolidayByNameComponent } from './holiday-by-name/holiday-by-name.component';

@NgModule({
  declarations: [
    AppComponent,
    DiffComponent,
    CountDaysWeekComponent,
    HolidayByNameComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

