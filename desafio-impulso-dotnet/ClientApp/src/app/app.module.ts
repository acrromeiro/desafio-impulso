import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NotifierModule } from "angular-notifier";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CreateSchoolComponent } from './create-school/create-school.component';
import { CreateSchoolClassComponent } from './create-school-class/create-school-class.component';
import { ListSchoolComponent } from './list-school/list-school.component';
import { ShowSchoolComponent } from './show-school/show-school.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CreateSchoolComponent,
    CreateSchoolClassComponent,
    ListSchoolComponent,
    ShowSchoolComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    NotifierModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'create-school', component: CreateSchoolComponent },
      { path: 'list-school', component: ListSchoolComponent },
      { path: 'show-school/:id', component: ShowSchoolComponent },
      { path: 'create-school-class/:id', component: CreateSchoolClassComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
