import { Component, OnInit } from '@angular/core';
import { DateService } from '../services/date.service';
import { DayOfWeekCount } from '../models/date.models';
import { DateUtils } from '../utils/date.utils';
import { catchError, finalize, timeout } from 'rxjs/operators';
import { of } from 'rxjs';

interface WeekdayDisplay {
  label: string;
  count: number;
}

@Component({
  selector: 'app-count-days',
  templateUrl: './count-days-week.component.html',
  styleUrls: ['./count-days-week.component.css']
})
export class CountDaysWeekComponent implements OnInit {
  startDate: string | null = null;
  endDate: string | null = null;
  countResult: DayOfWeekCount | null = null;
  loading = false;
  error: string | null = null;

  private readonly weekdayOrder = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
  private readonly weekdayMap: Record<string, string> = {
    Monday: 'Segunda-feira',
    Tuesday: 'Terça-feira',
    Wednesday: 'Quarta-feira',
    Thursday: 'Quinta-feira',
    Friday: 'Sexta-feira',
    Saturday: 'Sábado',
    Sunday: 'Domingo'
  };

  constructor(private dateService: DateService) {}

  ngOnInit(): void {
    const today = new Date();
    const prior = new Date();
    prior.setDate(today.getDate() - 7);
    this.startDate = DateUtils.formatDateForInput(prior);
    this.endDate = DateUtils.formatDateForInput(today);
  }

  getCountDaysOfWeek(): void {
    if (!this.startDate || !this.endDate) {
      this.error = 'Por favor informe as duas datas.';
      return;
    }

    this.loading = true;
    this.error = null;
    this.countResult = null;

    this.dateService.getCountDaysOfWeek(this.startDate, this.endDate)
      .pipe(
        timeout(15000),
        catchError((err) => {
          console.error('Error in getCountDaysOfWeek', err);
          this.error = 'Erro ao chamar o servidor.';
          return of(null);
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe((res) => {
        if (res) this.countResult = res as DayOfWeekCount;
      });
  }

  get weekdayDisplayList(): WeekdayDisplay[] {
    if (!this.countResult) return [];
    const src = this.countResult as Record<string, any>;

    const list: WeekdayDisplay[] = [];

    for (const day of this.weekdayOrder) {
      const pascal = day; // Monday
      const camel = day.charAt(0).toLowerCase() + day.slice(1); // monday
      const lower = day.toLowerCase(); // monday

      let foundKey: string | undefined;
      if (pascal in src) foundKey = pascal;
      else if (camel in src) foundKey = camel;
      else if (lower in src) foundKey = lower;

      if (foundKey) {
        const value = src[foundKey];
        if (typeof value === 'number') {
          list.push({ label: this.weekdayMap[day], count: value });
        }
      }
    }

    return list;
  }
}
