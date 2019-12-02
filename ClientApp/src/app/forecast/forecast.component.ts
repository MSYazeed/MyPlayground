import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl } from '@angular/forms';
import { debounceTime, tap, switchMap, finalize } from 'rxjs/operators';

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html'
})
export class ForecastComponent {
  public forecasts: WeatherForecast[];
  searchCitiesCtrl = new FormControl('');
  cities: any;
  city: object[];
  isSelected: boolean;
  isLoading = false;
  errorMsg: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    this.searchCitiesCtrl.valueChanges
      .pipe(
        debounceTime(500),
        tap(() => {
          this.errorMsg = "";
          this.cities = [];
          this.isLoading = true;
        }),
        switchMap(value => this.http.get("https://transit.api.here.com/v3/coverage/search.json?app_id=vRVIYx2z52djI1Kzveao&app_code=JYbQszFTaV-PM66WdXOlXw&max=10&details=1&politicalview=CHN&lang=en&name=" + value)
          .pipe(
            finalize(() => {
              this.isLoading = false;
            })
          )
        )
      )
      .subscribe(data => {
        if (data.Res.Coverage == null) {
          this.errorMsg = "can't find any city with that name";
          this.cities = [];
        } else {
          this.errorMsg = "";
          this.cities = data.Res.Coverage.Cities.City;
        }
        console.log(this.city);
      });
  }

  onCitySelected($event){
    console.log($event);
  }

  getForecast() {
    console.log(this.city);
    this.http.get<WeatherForecast[]>(this.baseUrl + 'api/forecast/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}


interface WeatherForecast {
  dateFormatted: string;
  temperatureCMin: number;
  temperatureCMax: number;
  summary: string;
}
