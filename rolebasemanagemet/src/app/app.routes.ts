import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';

export const routes: Routes = [
    {path:'dash', component:DashboardComponent},
    {path:'',loadChildren:()=>import('./auth/auth-routing.module').then(m=>m.AuthRoutingModule)},
   

];
