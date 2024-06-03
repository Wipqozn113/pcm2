import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Session {
  sessionDate: string;
  overview: string;
  notes: string;
  encounters: Encounter[];
}

interface Encounter {
  id: number;
  name: string;
  gold: number;
  partyLevel: number;
  difficulty: string;
  dashboardExport: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public nextSession!: Session;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
  }

  getForecasts() {
    this.http.get<Session>('/session').subscribe(
      (result) => {
        this.nextSession = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'pcm.client';
}
