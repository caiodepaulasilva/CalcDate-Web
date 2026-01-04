import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CacheService {
  setCache(key: string, value: any, hoursValid: number): void {
    try {
      const payload = { v: value, e: Date.now() + hoursValid * 3600 * 1000 };
      localStorage.setItem(key, JSON.stringify(payload));
    } catch {
      // Silently ignore storage errors
    }
  }

  getCache<T>(key: string): T | null {
    try {
      const raw = localStorage.getItem(key);
      if (!raw) return null;
      
      const payload = JSON.parse(raw);
      if (!payload || !payload.e || Date.now() > payload.e) {
        localStorage.removeItem(key);
        return null;
      }
      return payload.v as T;
    } catch {
      return null;
    }
  }

  clearCache(key: string): void {
    try {
      localStorage.removeItem(key);
    } catch {
      // Silently ignore
    }
  }
}
