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

  copyMessage(val: string) {
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = val;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
  }

  title = 'pcm.client';
}
