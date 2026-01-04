export class DateUtils {
  static formatDateForInput(date: Date): string {
    return date.toISOString().slice(0, 10);
  }

  static getDateWeekdayInPortuguese(dateString: string): string {
    try {
      // Parse date string as YYYY-MM-DD to avoid timezone issues
      const parts = dateString.split('T')[0].split('-');
      if (parts.length === 3) {
        const year = parseInt(parts[0], 10);
        const month = parseInt(parts[1], 10) - 1; // Month is 0-indexed
        const day = parseInt(parts[2], 10);
        const date = new Date(year, month, day);
        if (isNaN(date.getTime())) return '';
        return this.weekdayNumberToPortuguese(date.getDay());
      }
      
      // Fallback to direct parsing
      const date = new Date(dateString);
      if (isNaN(date.getTime())) return '';
      return this.weekdayNumberToPortuguese(date.getDay());
    } catch {
      return '';
    }
  }

  static weekdayNumberToPortuguese(dayNumber: number): string {
    const weekdays = [
      'Domingo',
      'Segunda-feira',
      'Terça-feira',
      'Quarta-feira',
      'Quinta-feira',
      'Sexta-feira',
      'Sábado'
    ];
    return weekdays[dayNumber] || '';
  }

  static formatDateDifference(diff: { years: number; months: number; days: number }): string {
    const parts: string[] = [];
    
    if (diff.years > 0) {
      parts.push(`${diff.years} ${diff.years === 1 ? 'ano' : 'anos'}`);
    }
    if (diff.months > 0) {
      parts.push(`${diff.months} ${diff.months === 1 ? 'mês' : 'meses'}`);
    }
    if (diff.days > 0) {
      parts.push(`${diff.days} ${diff.days === 1 ? 'dia' : 'dias'}`);
    }
    
    if (parts.length === 0) return '0 dias';
    if (parts.length === 1) return parts[0];
    if (parts.length === 2) return `${parts[0]} e ${parts[1]}`;
    
    return `${parts.slice(0, -1).join(', ')} e ${parts[parts.length - 1]}`;
  }
}
