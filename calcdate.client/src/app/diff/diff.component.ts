import { Component, OnInit } from '@angular/core';
import { DateService } from '../services/date.service';
import { DateDifference } from '../models/date.models';
import { DateUtils } from '../utils/date.utils';

@Component({
  selector: 'app-diff',
  templateUrl: './diff.component.html',
  styleUrls: ['./diff.component.css']
})
export class DiffComponent implements OnInit {
  diffResult: DateDifference | null = null;
  startDate: string | null = null;
  endDate: string | null = null;
  loading = false;
  error: string | null = null;

  constructor(private dateService: DateService) {}

  ngOnInit(): void {
    const today = new Date();
    const prior = new Date();
    prior.setDate(today.getDate() - 7);
    this.startDate = DateUtils.formatDateForInput(prior);
    this.endDate = DateUtils.formatDateForInput(today);
  }

  getDiffBetweenDates(): void {
    if (!this.startDate || !this.endDate) {
      this.error = 'Por favor informe as duas datas.';
      return;
    }

    this.loading = true;
    this.error = null;
    this.diffResult = null;

    this.dateService.getDiffBetweenDates(this.startDate, this.endDate).subscribe({
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

  get diffResultDisplay(): string {
    return this.diffResult ? DateUtils.formatDateDifference(this.diffResult) : '';
  }
}
