import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl } from '@angular/forms';
import { debounceTime, tap, switchMap, finalize, filter } from 'rxjs/operators';

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html'
})
export class ForecastComponent {
  public forecasts: WeatherForecast[];
  searchCitiesCtrl = new FormControl('');
  cities: City[];
  isSelected: boolean;
  isLoading = false;
  errorMsg: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
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
          `api/city/search/${value}`/*"https://transit.api.here.com/v3/coverage/search.json?app_id=vRVIYx2z52djI1Kzveao&app_code=JYbQszFTaV-PM66WdXOlXw&max=10&details=1&politicalview=CHN&lang=en&name=" + value*/)
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

  //onCitySelected($event){
  //  console.log($event);
  //}

  getForecast() {
    if (this.searchCitiesCtrl.value) {
      this.http.get<WeatherForecast[]>(this.baseUrl +
        `api/forecast/search/${this.searchCitiesCtrl.value.lat},${this.searchCitiesCtrl.value.long}`).subscribe(result => {
          this.forecasts = result;
        },
        error => console.error(error));
    }
  }

  displayFn(val: City) {
    return val ? val.name + ', ' + val.countryCode : val;
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureCMin: number;
  temperatureCMax: number;
  summary: string;
}

interface City {
  id: number;
  name: string;
  countryCode: string;
  lat: number;
  long: number;
}

