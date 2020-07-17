import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';


import { routing } from './app.routing';

import { AppComponent } from './app.component';
import { AdminComponent } from './admin/admin.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { JwtInterceptor, ErrorInterceptor, fakeBackendProvider } from './_helpers';
import { AddNeedComponent } from './add-need/add-need.component';
import { NeedsListComponent } from './needs-list/needs-list.component';
import { MyNeedsComponent } from './my-needs/my-needs.component';
import { RegisterComponent } from './register/register.component';
import { MapComponent } from './map/map.component';
import { NeedsOverviewComponent } from './needs-overview/needs-overview.component';
import { NeedDetailsComponent } from './need-details/need-details.component'; // used to create fake backend


@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    HomeComponent,
    LoginComponent,
    AddNeedComponent,
    NeedsListComponent,
    MyNeedsComponent,
    RegisterComponent,
    MapComponent,
    NeedsOverviewComponent,
    NeedDetailsComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    routing
  ],
  providers: [
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

      // provider used to create fake backend
      fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
