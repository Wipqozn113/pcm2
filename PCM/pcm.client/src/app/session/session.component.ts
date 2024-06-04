import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface Session {
  sessionDate: string;
  overview: string;
  notes: string;
  encounters: Encounter[];
  loot: Loot[];
}

interface Encounter {
  id: number;
  name: string;
  gold: number;
  partyLevel: number;
  difficulty: string;
  dashboardExport: string;
}

interface Loot {
  name: string;
  itemLevel: number;
  aoNUrl: string;
  editUrl: string;
}

@Component({
  selector: 'app-session',
  templateUrl: './session.component.html',
  styleUrl: './session.component.css'
})
export class SessionComponent {
  public nextSession!: Session;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getNextSession();
  }

  getNextSession() {
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

