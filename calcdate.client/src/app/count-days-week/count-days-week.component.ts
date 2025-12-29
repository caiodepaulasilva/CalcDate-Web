import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-count-days',
  templateUrl: './count-days-week.component.html',
  styleUrls: ['./count-days-week.component.css']
})
export class CountDaysWeekComponent implements OnInit {
  public startDate: string | null = null;
  public endDate: string | null = null;
  public dayOfWeek: number = 1;
  // can be number or object result from server
  public countResult: any = null;
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

  getCountDaysOfWeek() {
    if (!this.startDate || !this.endDate) {
      this.error = 'Por favor informe as duas datas.';
      return;
    }

    this.loading = true;
    this.error = null;
    this.countResult = null;

    const params = new HttpParams()
      .set('StartDate', this.startDate)
      .set('EndDate', this.endDate)      

    this.http.get<any>('/DayCalculations/CountDaysOfWeek', { params }).subscribe({
      next: (res) => {
        this.countResult = res;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Erro ao chamar o servidor.';
        this.loading = false;
      }
    });
  }

  get countResultDisplay(): string {
    if (this.countResult === null || this.countResult === undefined) return '';

    // If server returns a plain number
    if (typeof this.countResult === 'number') return String(this.countResult);

    // If server returns an object, try common property names
    const obj = this.countResult as Record<string, any>;

    // Prefer common single-value properties
    const candidates = ['Total', 'total', 'Count', 'count', 'Quantity', 'quantity', 'Value', 'value'];
    for (const key of candidates) {
      if (key in obj && (typeof obj[key] === 'number' || typeof obj[key] === 'string')) {
        return String(obj[key]);
      }
    }

    // If object has a toString result from server it won't be serialized; fallback to building a readable string
    const parts: string[] = [];
    for (const [k, v] of Object.entries(obj)) {
      if (v === null || v === undefined) continue;
      if (typeof v === 'object') {
        // skip complex nested objects in summary
        continue;
      }
      parts.push(`${k}: ${v}`);
    }

    if (parts.length > 0) return parts.join(' • ');

    // last resort: JSON
    try {
      return JSON.stringify(this.countResult);
    } catch {
      return String(this.countResult);
    }
  }

  get countResultEntries(): [string, any][] {
    if (this.countResult === null || typeof this.countResult !== 'object') return [];
    return Object.entries(this.countResult);
  }
}
