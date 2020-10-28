import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-list-school',
  templateUrl: './list-school.component.html'
})
export class ListSchoolComponent {
  public schools: School[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<School[]>(baseUrl + 'school').subscribe(result => {
      this.schools = result;
    }, error => console.error(error));
  }
}

interface School {
  name: string;
}
