import {Component, Inject, Input} from '@angular/core';
import {Router} from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { NotifierService } from "angular-notifier";

@Component({
  selector: 'app-create-school-component',
  templateUrl: './create-school.component.html'
})
export class CreateSchoolComponent {
  @Input()
  public name: string;

  constructor(private notifier:NotifierService,private router: Router, private http: HttpClient, @Inject('API_BASE_URL')private baseUrl: string) {}

  public save() {
    this.http.post(this.baseUrl + 'school',{"name":this.name}).subscribe(result => {
      if(result === 201) {
        this.notifier.notify("sucess","Cadastro de escola feito com sucesso!");
        this.router.navigate(['/list-school']).then(r => {});
      }
    }, error => {this.notifier.notify("error","Falha ao cadastrar escola!!"); console.error(error);});
  }
}
