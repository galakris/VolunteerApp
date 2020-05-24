import { RegisterComponent } from './register/register.component';
import { AddNeedComponent } from './add-need/add-need.component';
import { MyNeedsComponent } from './my-needs/my-needs.component';
import { NeedsListComponent } from './needs-list/needs-list.component';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { AdminComponent } from './admin';
import { AuthGuardService } from './_guards';
import { LoginComponent } from './login';
import { Role } from './_models/role';

const appRoutes: Routes = [
    {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuardService]
    },
    {
        path: 'admin',
        component: AdminComponent,
        canActivate: [AuthGuardService],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
      path: 'needs',
      component: NeedsListComponent,
      canActivate: [AuthGuardService],
      data: { roles: [Role.Volunteer, Role.Admin]}
    },
    {
      path: 'my-needs',
      component: MyNeedsComponent,
      canActivate: [AuthGuardService],
      data: { roles: [Role.Needy]}
    },
    {
      path: 'add-need',
      component: AddNeedComponent,
      canActivate: [AuthGuardService],
      data: {roles: [Role.Needy]}
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
