import { NgModule } from '@angular/core';
import { RouterModule, Routes} from '@angular/router';
import { EncounterComponent } from './encounter/encounter.component';
import { SessionComponent } from './session/session.component';

const routes: Routes = [
  { path: 'enc/:id', component: EncounterComponent },
  { path: 'ses', component: SessionComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
