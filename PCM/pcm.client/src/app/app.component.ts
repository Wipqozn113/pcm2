import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface CampaignInfo {
  partyLevel: number;
  experience: number;
  goldAwardedThisLevel: number;
  totalGoldForThisLevel: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public campaignInfo!: CampaignInfo;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
  }

  getForecasts() {
    this.http.get<CampaignInfo>('/CampaignInfo').subscribe(
      (result) => {
        this.campaignInfo = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'pcm.client';
}
