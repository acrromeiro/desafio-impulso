import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-show-school',
  templateUrl: './show-school.component.html'
})
export class ShowSchoolComponent {
  public schoolClasses: SchoolClass[];
  public schoolId: string;

  constructor(private route: ActivatedRoute,http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.schoolId = this.route.snapshot.paramMap.get('id');

    http.get<SchoolClass[]>(baseUrl + 'school/' + this.schoolId ).subscribe(result => {
      this.schoolClasses = result;
    }, error => console.error(error));
  }
}

interface SchoolClass {
  id: number;
  name: string;
  grade: string;
  qtdStudents: number;
}
