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

  private normalizeCountResult(res: any): any {
    if (res === null || res === undefined) return res;
    if (typeof res === 'number') return res;

    const weekdays = ['Monday','Tuesday','Wednesday','Thursday','Friday','Saturday','Sunday'];

    // If server returned an array of entries, try to convert to an object
    if (Array.isArray(res)) {
      const obj: Record<string, any> = {};
      for (const item of res) {
        if (item === null || item === undefined) continue;
        if (Array.isArray(item) && item.length >= 2) {
          obj[item[0]] = item[1];
          continue;
        }
        if (typeof item === 'object') {
          const keys = Object.keys(item);
          // try to detect day/name and value/count keys
          const dayKey = keys.find(k => ['day','Day','name','Name','weekday','WeekDay'].includes(k));
          const valueKey = keys.find(k => ['count','Count','value','Value','total','Total','quantity','Quantity'].includes(k));
          if (dayKey && valueKey) {
            obj[item[dayKey]] = item[valueKey];
            continue;
          }

          // fallback: if object has one string and one number property
          const stringKey = keys.find(k => typeof item[k] === 'string');
          const numberKey = keys.find(k => typeof item[k] === 'number');
          if (stringKey && numberKey) {
            obj[item[stringKey]] = item[numberKey];
            continue;
          }
        }
      }
      return obj;
    }

    // If it's already an object, just return it
    if (typeof res === 'object') return res;

    return res;
  }

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

    this.http.get<any>('/api/v1/Date/CountDaysOfWeek', { params }).subscribe({
      next: (res) => {
        console.log('raw count result', res);
        this.countResult = this.normalizeCountResult(res);
        console.log('normalized count result', this.countResult);
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

    // Prefer a fixed Monday..Sunday order and translate keys to Portuguese
    const dayMap: Record<string, string> = {
      Monday: 'Segunda-feira',
      Tuesday: 'Terça-feira',
      Wednesday: 'Quarta-feira',
      Thursday: 'Quinta-feira',
      Friday: 'Sexta-feira',
      Saturday: 'Sábado',
      Sunday: 'Domingo'
    };

    const order = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];

    const entries: [string, any][] = [];
    const matchedKeys = new Set<string>();

    // For each weekday, try several key variants to be robust against JSON naming policies
    for (const key of order) {
      const pascal = key; // Monday
      const camel = key.charAt(0).toLowerCase() + key.slice(1); // monday
      const lower = key.toLowerCase(); // monday (same as camel for single-word)

      const src = this.countResult as Record<string, any>;
      let foundKey: string | undefined;

      if (pascal in src) foundKey = pascal;
      else if (camel in src) foundKey = camel;
      else if (lower in src) foundKey = lower;

      if (foundKey) {
        entries.push([dayMap[key], src[foundKey]]);
        matchedKeys.add(foundKey);
      }
    }

    // Append any other properties returned by server that are not matched weekdays
    for (const [k, v] of Object.entries(this.countResult)) {
      if (!matchedKeys.has(k)) entries.push([k, v]);
    }

    return entries;
  }
}
