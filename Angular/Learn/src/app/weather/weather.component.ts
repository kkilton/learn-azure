import { HttpClient } from '@angular/common/http';
import { Component, Inject, LOCALE_ID } from '@angular/core';
import { formatDate } from '@angular/common';
import { WeatherForecast } from '../models/weather-forecast';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.sass']
})
export class WeatherComponent {
  public weatherForecasts!: WeatherForecast[];
  public numberOfDays: number = 5;

  constructor(
    private http: HttpClient,
    @Inject(LOCALE_ID) public locale: string
  ) {}

  getWeather() {
    this.http
      .get<WeatherForecast[]>(
        `http://localhost:5289/api/WeatherForecast?numberOfDays=${this.numberOfDays}`
      )
      .subscribe((wf) => {
        this.weatherForecasts = wf;
        console.log(this.weatherForecasts);
      });
  }

  formatDate(date: Date): string {
    return formatDate(date, 'yyyy-MM-dd', this.locale);
  }
}
