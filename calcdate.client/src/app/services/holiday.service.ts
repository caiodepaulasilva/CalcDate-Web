import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Holiday, LocationOption } from '../models/holiday.models';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class HolidayService {
  private readonly baseUrl = '/api/v1/Holiday';
  private readonly locationsCacheKey = 'holiday_locations_v1';

  constructor(
    private http: HttpClient,
    private cacheService: CacheService
  ) {}

  getLocations(): Observable<LocationOption[]> {
    return this.http.get<LocationOption[]>(`${this.baseUrl}/locations`);
  }

  getCachedLocations(): LocationOption[] | null {
    return this.cacheService.getCache<LocationOption[]>(this.locationsCacheKey);
  }

  cacheLocations(locations: LocationOption[]): void {
    this.cacheService.setCache(this.locationsCacheKey, locations, 24);
  }

  getHolidaysByLocation(year: number, location: string): Observable<Holiday[]> {
    const params = new HttpParams()
      .set('Year', String(year))
      .set('Location', location);

    return this.http.get<Holiday[]>(`${this.baseUrl}/name`, { params });
  }
}
