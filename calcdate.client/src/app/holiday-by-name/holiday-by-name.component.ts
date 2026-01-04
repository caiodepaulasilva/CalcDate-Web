import { Component, OnInit } from '@angular/core';
import { HolidayService } from '../services/holiday.service';
import { Holiday, LocationOption } from '../models/holiday.models';

@Component({
  selector: 'app-holiday-by-name',
  templateUrl: './holiday-by-name.component.html',
  styleUrls: ['./holiday-by-name.component.css']
})
export class HolidayByNameComponent implements OnInit {
  holidayResults: Holiday[] = [];
  selectedLocation = 'Nacional';
  year = new Date().getFullYear();
  loading = false;
  error: string | null = null;
  locations: LocationOption[] = [];

  constructor(private holidayService: HolidayService) {}

  async ngOnInit(): Promise<void> {
    await this.loadLocations();
    await this.loadHolidays();
  }

  private async loadLocations(): Promise<void> {
    const cached = this.holidayService.getCachedLocations();
    if (cached) {
      this.locations = cached;
      return;
    }

    return new Promise<void>((resolve) => {
      this.holidayService.getLocations().subscribe({
        next: (locations) => {
          this.locations = locations;
          this.holidayService.cacheLocations(locations);
          resolve();
        },
        error: () => {
          this.locations = this.getDefaultLocations();
          resolve();
        }
      });
    });
  }

  loadAllHolidays(): void {
    this.loadHolidays();
  }

  onLocationChangeEvent(): void {
    this.loadHolidays();
  }

  private loadHolidays(): Promise<void> {
    this.loading = true;
    this.error = null;
    this.holidayResults = [];

    return new Promise((resolve) => {
      this.holidayService.getHolidaysByLocation(this.year, this.selectedLocation).subscribe({
        next: (holidays) => {
          this.holidayResults = this.processHolidays(holidays);
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

  private processHolidays(holidays: any[]): Holiday[] {
    // Usa diretamente os dados do servidor sem processamento adicional
    const items = holidays.map(item => ({
      date: item.date || item.Date || '',
      name: String(item.name ?? item.Name ?? '').trim(),
      local: String(item.local ?? item.Local ?? item.location ?? item.Location ?? '').trim(),
      weekday: String(item.dayOfWeek ?? item.DayOfWeek ?? '').trim() // Usa diretamente do servidor
    }));

    // Remove duplicatas
    const seen = new Set<string>();
    return items.filter(item => {
      const key = `${item.date}|${item.name.toLowerCase()}|${item.local.toLowerCase()}`;
      if (seen.has(key)) return false;
      seen.add(key);
      return true;
    });
  }

  private getDefaultLocations(): LocationOption[] {
    return [
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
  }
}
