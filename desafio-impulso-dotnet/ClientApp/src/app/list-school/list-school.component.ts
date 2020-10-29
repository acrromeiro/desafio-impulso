import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-list-school',
  templateUrl: './list-school.component.html'
})
export class ListSchoolComponent {
  public schools: School[];

  constructor(http: HttpClient, @Inject('API_BASE_URL') baseUrl: string) {
    console.log(baseUrl)
    http.get<School[]>(baseUrl + 'school').subscribe(result => {
      this.schools = result;
    }, error => console.error(error));
  }
}

interface School {
  id: number
  name: string;
}
