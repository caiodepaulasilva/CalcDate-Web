import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface DiffBetweenDatesResult {
  years: number;
  months: number;
  days: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public diffResult: DiffBetweenDatesResult | null = null;
  public startDate: string | null = null;
  public endDate: string | null = null;
  public loading = false;
  public error: string | null = null;

  // CountDaysOfWeek specific
  public dayOfWeek: number = 1; // 0 = Sunday, 1 = Monday, ...
  public countResult: number | null = null;
  public loadingCount = false;
  public errorCount: string | null = null;

  public isDark = true;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    // optional: set default dates
    const today = new Date();
    const prior = new Date();
    prior.setDate(today.getDate() - 7);
    this.startDate = prior.toISOString().slice(0, 10);
    this.endDate = today.toISOString().slice(0, 10);

    // theme: default dark, allow persisted preference
    const stored = localStorage.getItem('theme');
    if (stored) {
      this.isDark = stored === 'dark';
    } else {
      this.isDark = true; // default dark
    }
    this.applyTheme();
  }

  applyTheme() {
    if (this.isDark) {
      document.body.classList.remove('light-theme');
    } else {
      document.body.classList.add('light-theme');
    }
    localStorage.setItem('theme', this.isDark ? 'dark' : 'light');
  }

  toggleTheme() {
    this.isDark = !this.isDark;
    this.applyTheme();
  }

  getDiffBetweenDates() {
    if (!this.startDate || !this.endDate) {
      this.error = 'Por favor informe as duas datas.';
      return;
    }

    this.loading = true;
    this.error = null;
    this.diffResult = null;

    const params = new HttpParams()
      .set('StartDate', this.startDate)
      .set('EndDate', this.endDate);

    // Call the Date controller
    this.http.get<DiffBetweenDatesResult>('/api/v1/Date/DiffBetweenDates', { params }).subscribe({
      next: (res) => {
        this.diffResult = res;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Erro ao chamar o servidor.';
        this.loading = false;
      }
    });
  }

  getCountDaysOfWeek() {
    if (!this.startDate || !this.endDate) {
      this.errorCount = 'Por favor informe as duas datas.';
      return;
    }

    this.loadingCount = true;
    this.errorCount = null;
    this.countResult = null;

    const params = new HttpParams()
      .set('StartDate', this.startDate)
      .set('EndDate', this.endDate)
      .set('DayOfWeek', String(this.dayOfWeek));

    this.http.get<number>('/api/v1/Date/CountDaysOfWeek', { params }).subscribe({
      next: (res) => {
        this.countResult = res;
        this.loadingCount = false;
      },
      error: (err) => {
        console.error(err);
        this.errorCount = 'Erro ao chamar o servidor.';
        this.loadingCount = false;
      }
    });
  }

  get diffResultDisplay() {
    if (!this.diffResult) return '';
    const parts: string[] = [];
    if (this.diffResult.years > 0) parts.push(`${this.diffResult.years} ${this.diffResult.years === 1 ? 'ano' : 'anos'}`);
    if (this.diffResult.months > 0) parts.push(`${this.diffResult.months} ${this.diffResult.months === 1 ? 'mês' : 'meses'}`);
    if (this.diffResult.days > 0) parts.push(`${this.diffResult.days} ${this.diffResult.days === 1 ? 'dia' : 'dias'}`);
    if (parts.length === 0) return '0 dias';
    if (parts.length === 1) return parts[0];
    if (parts.length === 2) return `${parts[0]} e ${parts[1]}`;
    return `${parts.slice(0, parts.length - 1).join(', ')} e ${parts[parts.length - 1]}`;
  }

  title = 'calcdate.client';
}
