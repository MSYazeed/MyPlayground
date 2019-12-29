export interface WeatherForecast {
    currentDateTimeFormatted: string;
    currentTemperatureC: number;
    currentHumidity: number;
    currentWindSpeed: number;
    currentSummary: string;
    currentCondition: string;
    daysForecast: [{
        day: string;
        temperatureCMin: number;
        temperatureCMax: number;
        summary: string;
        condition: string;
    }];
}

export enum WeatherCondition {
    ClearDay = 'clear-day',
    ClearNight = 'clear-night',
    Rain = 'rain',
    Snow = 'snow',
    Sleet = 'sleet',
    Wind = 'wind',
    Fog = 'fog',
    Cloudy = 'cloudy',
    PartlyCloudyDay = 'partly-cloudy-day',
    PartlyCloudyNight = 'partly-cloudy-night',
    Thunderstorm = 'thunderstorm'
}
