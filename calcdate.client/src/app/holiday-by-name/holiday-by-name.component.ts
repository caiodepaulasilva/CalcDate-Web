import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-holiday-by-name',
  templateUrl: './holiday-by-name.component.html',
  styleUrls: ['./holiday-by-name.component.css']
})
export class HolidayByNameComponent implements OnInit {
public holidayResults: { date: string; name: string }[] = [];
// default to first location (Nacional)
public selectedLocation: string = 'Nacional';
public year: number = new Date().getFullYear();
public loading = false;
public error: string | null = null;

  public locations = [
    { value: 'Nacional', label: 'Nacional' },
    { value: 'Acre', label: 'Acre' },
    { value: 'Alagoas', label: 'Alagoas' },
    { value: 'Amapa', label: 'Amapá' },
    { value: 'Amazonas', label: 'Amazonas' },
    { value: 'Bahia', label: 'Bahia' },
    { value: 'Ceara', label: 'Ceará' },
    { value: 'DistritoFederal', label: 'Distrito Federal' },
    { value: 'EspiritoSanto', label: 'Espírito Santo' },
    { value: 'Goias', label: 'Goiás' },
    { value: 'Maranhao', label: 'Maranhão' },
    { value: 'MatoGrosso', label: 'Mato Grosso' },
    { value: 'MatoGrossoDoSul', label: 'Mato Grosso do Sul' },
    { value: 'MinasGerais', label: 'Minas Gerais' },
    { value: 'Para', label: 'Pará' },
    { value: 'Paraiba', label: 'Paraíba' },
    { value: 'Parana', label: 'Paraná' },
    { value: 'Pernambuco', label: 'Pernambuco' },
    { value: 'Piaui', label: 'Piauí' },
    { value: 'RioDeJaneiro', label: 'Rio de Janeiro' },
    { value: 'RioGrandeDoNorte', label: 'Rio Grande do Norte' },
    { value: 'RioGrandeDoSul', label: 'Rio Grande do Sul' },
    { value: 'Rondonia', label: 'Rondônia' },
    { value: 'Roraima', label: 'Roraima' },
    { value: 'SantaCatarina', label: 'Santa Catarina' },
    { value: 'SaoPaulo', label: 'São Paulo' },
    { value: 'Sergipe', label: 'Sergipe' },
    { value: 'Tocantins', label: 'Tocantins' }
  ];

  constructor(private http: HttpClient) {}

  async ngOnInit(): Promise<void> {
    // load locations dynamically from server (uses cache)
    await this.loadLocations();
    // load holidays for initial selection
    await this.loadAllHolidays();
  }

  async loadLocations(): Promise<void> {
    const cacheKey = 'holiday_locations_v1';
    const cached = this.getCache(cacheKey);
    if (cached) {
      this.locations = (cached || []).map((s: any) => ({ value: s.value, label: s.label }));
      this.selectedLocation = this.locations.length ? this.locations[0].value : 'Nacional';
      return;
    }
    return new Promise<void>((resolve) => {
      this.http.get<any[]>('/api/v1/Holiday/locations').subscribe({
        next: async (res) => {
          this.locations = (res || []).map(s => ({ value: s.value, label: s.label }));
          this.selectedLocation = this.locations.length ? this.locations[0].value : 'Nacional';
          this.setCache(cacheKey, res, 24); // cache 24 hours
          resolve();
        },
        error: () => {
          // keep existing fallback locations
          this.selectedLocation = this.locations.length ? this.locations[0].value : 'Nacional';
          resolve();
        }
      });
    });
  }

  loadAllHolidays(locationOverride?: string): Promise<void> {
    const locationToUse = ((locationOverride ?? this.selectedLocation ?? 'Nacional') || 'Nacional').trim();
    // Don't send Name parameter at all when empty (cleaner than sending empty string)
    const params = new HttpParams()
      .set('Year', String(this.year))
      .set('Location', locationToUse);
    
    console.debug('Loading holidays', { year: this.year, location: locationToUse });
    
    return new Promise((resolve) => {
      this.loading = true;
      this.error = null;
      this.holidayResults = [];
      
      this.http.get<any[]>('/api/v1/Holiday/name', { params }).subscribe({
        next: (res) => {
          const items = (res || []).map(i => ({ 
            date: i.date || i.Date || '',
            name: (i.name || i.Name || '').trim()
          }));

          // Remove duplicates by date+name
          const seen = new Set<string>();
          this.holidayResults = items.filter(it => {
            const key = `${it.date}|${it.name.toLowerCase()}`;
            if (seen.has(key)) return false;
            seen.add(key);
            return true;
          });

          console.debug('[holiday] loaded', { count: this.holidayResults.length });
          this.loading = false;
          resolve();
        },
        error: (err) => {
          console.error(err);
          this.error = 'Erro ao carregar feriados.';
          this.loading = false;
          resolve();
        }
      });
    });
  }

  async onLocationChange(newLocation?: string): Promise<void> {
    if (newLocation) this.selectedLocation = newLocation;
    console.debug('[holiday] onLocationChange called', { newLocation, selectedLocation: this.selectedLocation });
    await this.loadAllHolidays(newLocation);
  }

  onLocationChangeEvent(): void {
    console.debug('[holiday] onLocationChangeEvent fired', { selectedLocation: this.selectedLocation });
    this.onLocationChange(this.selectedLocation);
  }

  // simple localStorage cache helper
  private setCache(key: string, value: any, hoursValid: number) {
    try {
      const payload = { v: value, e: Date.now() + hoursValid * 3600 * 1000 };
      localStorage.setItem(key, JSON.stringify(payload));
    } catch { /* ignore */ }
  }

  private getCache(key: string) {
    try {
      const raw = localStorage.getItem(key);
      if (!raw) return null;
      const payload = JSON.parse(raw);
      if (!payload || !payload.e || Date.now() > payload.e) {
        localStorage.removeItem(key);
        return null;
      }
      return payload.v;
    } catch {
      return null;
    }
  }
}
