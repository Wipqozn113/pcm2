import { NgModule } from '@angular/core';
import { RouterModule, Routes} from '@angular/router';
import { EncounterComponent } from './encounter/encounter.component';
import { SessionComponent } from './session/session.component';

const routes: Routes = [
  { path: '', component: SessionComponent },
  { path: 'enc/:id', component: EncounterComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
