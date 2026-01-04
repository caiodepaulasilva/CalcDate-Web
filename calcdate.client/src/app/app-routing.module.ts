import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
