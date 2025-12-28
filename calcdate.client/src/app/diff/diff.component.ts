import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

interface DiffBetweenDatesResult {
  years: number;
  months: number;
  days: number;
}

@Component({
  selector: 'app-diff',
  templateUrl: './diff.component.html',
  styleUrls: ['./diff.component.css']
})
export class DiffComponent implements OnInit {
  public diffResult: DiffBetweenDatesResult | null = null;
  public startDate: string | null = null;
  public endDate: string | null = null;
  public loading = false;
  public error: string | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const today = new Date();
    const prior = new Date();
    prior.setDate(today.getDate() - 7);
    this.startDate = prior.toISOString().slice(0, 10);
    this.endDate = today.toISOString().slice(0, 10);
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

    this.http.get<DiffBetweenDatesResult>('/DayCalculations/DiffBetweenDates', { params }).subscribe({
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
}
