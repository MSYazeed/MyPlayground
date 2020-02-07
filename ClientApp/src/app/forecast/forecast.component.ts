import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl } from '@angular/forms';
import { debounceTime, tap, switchMap, finalize, filter } from 'rxjs/operators';
import { City } from './City';
import { WeatherForecast, WeatherCondition } from './WeatherForecast';

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html',
  styleUrls: ['./forecast.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class ForecastComponent {
  forecasts: WeatherForecast;
  conditions = WeatherCondition;
  searchCitiesCtrl = new FormControl('');
  cities: City[];
  isSelected: boolean;
  isLoading = false;
  errorMsg: string;
  celsius: boolean;
  
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.celsius = true;
  }

  ngOnInit() {
    this.searchCitiesCtrl.valueChanges
      .pipe(
        filter(x => typeof x === 'string' && x.length >= 3),
        debounceTime(500),
        tap(() => {
          this.errorMsg = "";
          this.cities = [];
          this.isLoading = true;
        }),
        switchMap(value => this.http.get<City[]>(this.baseUrl +
          `api/city/search/${value}`
          /*"https://transit.api.here.com/v3/coverage/search.json?app_id=vRVIYx2z52djI1Kzveao&app_code=JYbQszFTaV-PM66WdXOlXw&max=10&details=1&politicalview=CHN&lang=en&name=" + value*/
        )
          .pipe(
            finalize(() => {
              this.isLoading = false;
            })
          )
        )
      )
      .subscribe(citiesList => {
        if (citiesList == null) {
          this.errorMsg = "can't find any city with that name";
          this.cities = [];
        } else {
          this.errorMsg = "";
          this.cities = citiesList;
        }
      });
  }
  
  getForecast() {
    if (this.searchCitiesCtrl.value) {
      this.http.get<WeatherForecast>(this.baseUrl +
        `api/forecast/search/${this.searchCitiesCtrl.value.lat},${this.searchCitiesCtrl.value.long}`).subscribe(result => {
          this.forecasts = result;
        },
          error => console.error(error));
    }
  }

  displayFn(val: City) {
    return val ? val.name + ', ' + val.countryCode : val;
  }

  convertTemperature(temperatureC){
    return this.celsius ? temperatureC : ((temperatureC * 9/5) + 32).toFixed(2);
  }
}


