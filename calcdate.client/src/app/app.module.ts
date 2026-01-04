import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DiffComponent } from './diff/diff.component';
import { CountDaysWeekComponent } from './count-days-week/count-days-week.component';
import { HolidayByNameComponent } from './holiday-by-name/holiday-by-name.component';

const routes: Routes = [
  { path: '', redirectTo: 'diff', pathMatch: 'full' },
  { path: 'diff', component: DiffComponent },
  { path: 'count-days-week', component: CountDaysWeekComponent },  
  { path: 'holiday-by-name', component: HolidayByNameComponent },
  { path: '**', redirectTo: 'diff' }
];

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
    AppRoutingModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
