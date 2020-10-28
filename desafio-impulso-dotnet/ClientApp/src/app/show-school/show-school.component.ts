import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-list-school',
  templateUrl: './list-school.component.html'
})
export class ShowSchoolComponent {
  public schoolClasses: SchoolClass[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<SchoolClass[]>(baseUrl + 'school').subscribe(result => {
      this.schoolClasses = result;
    }, error => console.error(error));
  }
}

interface SchoolClass {
  name: string;
  grade: string;
  qtdStudents: number;
}
