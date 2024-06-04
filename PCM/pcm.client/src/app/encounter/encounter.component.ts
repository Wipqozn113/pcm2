import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

interface Encounter {
  id: number;
  name: string;
  gold: number;
  partyLevel: number;
  difficulty: string;
  dashboardExport: string;
  monsters: Monster[];
  loot: Loot[];
}

interface Monster {
  name: string;
  link: string;
  quantity: number;
}

interface Loot {
  name: string;
  itemLevel: number;
  aoNUrl: string;
  editUrl: string;
}

@Component({
  selector: 'app-encounter',
  templateUrl: './encounter.component.html',
  styleUrl: './encounter.component.css'
})
export class EncounterComponent {
  public id!: number;
  private sub!: any;
  public encounter!: Encounter;

  constructor(private http: HttpClient, private route: ActivatedRoute) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number

      // In a real app: dispatch action to load the details here.
    });
    this.getEncounter();
  }

  getEncounter() {
    this.http.get<Encounter>('/Encounter?encounterId=' + this.id).subscribe(
      (result) => {
        this.encounter = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'pcm.client';
}
