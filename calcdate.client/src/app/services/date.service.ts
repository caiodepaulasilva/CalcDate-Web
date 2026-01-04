import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DateDifference, DayOfWeekCount } from '../models/date.models';

@Injectable({
  providedIn: 'root'
})
export class DateService {
  private readonly baseUrl = '/api/v1/Date';

  constructor(private http: HttpClient) {}

  getDiffBetweenDates(startDate: string, endDate: string): Observable<DateDifference> {
    const params = new HttpParams()
      .set('StartDate', startDate)
      .set('EndDate', endDate);

    return this.http.get<DateDifference>(`${this.baseUrl}/DiffBetweenDates`, { params });
  }

  getCountDaysOfWeek(startDate: string, endDate: string): Observable<DayOfWeekCount> {
    const params = new HttpParams()
      .set('StartDate', startDate)
      .set('EndDate', endDate);

    return this.http.get<DayOfWeekCount>(`${this.baseUrl}/CountDaysOfWeek`, { params });
  }
}
