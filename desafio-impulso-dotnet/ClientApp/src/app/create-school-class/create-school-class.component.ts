import {Component, Inject, Input} from '@angular/core';
import {Router,ActivatedRoute} from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { NotifierService } from "angular-notifier";

@Component({
  selector: 'app-create-school-class-component',
  templateUrl: './create-school-class.component.html'
})
export class CreateSchoolClassComponent {
  @Input()
  public name: string;
  @Input()
  public grade: string;
  @Input()
  public qtdStudents: string;
  public schoolId: string;

  constructor(private notifier:NotifierService,private route: ActivatedRoute,private router: Router, private http: HttpClient, @Inject('BASE_URL')private baseUrl: string) {
    this.schoolId = this.route.snapshot.paramMap.get('id');
  }

  public save() {
    const body = {"name":this.name, "grade": this.grade, "qtdStudents": this.qtdStudents, "schoolId": this.schoolId};

    this.http.post(this.baseUrl + 'school/'+ this.schoolId,body).subscribe(result => {
      if(result === 201) {
        this.notifier.notify("sucess","Cadastro de turma feito com sucesso!");
        this.router.navigate(['/list-school']).then(r => {});
      }
    }, error => {this.notifier.notify("error","Falha ao cadastrar Turma!!"); console.error(error);});
  }
}
