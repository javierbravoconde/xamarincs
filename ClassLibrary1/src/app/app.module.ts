import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import {LocationStrategy, HashLocationStrategy} from '@angular/common';

import {
    RouterModule,
    Routes
} from '@angular/router';

const routes: Routes = [
     { path: '', redirectTo: 'home', pathMatch: 'full' },
     { path: 'home', component: HomeComponent },
     { path: 'about', component: AboutComponent },
    ];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
      HttpModule,
      RouterModule.forRoot(routes) 
  ],
  providers: [{ provide: LocationStrategy, useClass: HashLocationStrategy }],
  bootstrap: [AppComponent]
})
export class AppModule { }
