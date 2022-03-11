import { Injectable } from '@angular/core';
import { CardDetail } from './card-detail.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserModel } from './user-model';

@Injectable({
  providedIn: 'root'
})
export class CardDetailService {

  constructor(private http: HttpClient, private fb: FormBuilder) { }

  readonly baseUrl = 'https://localhost:44340/api/CardDetails'
  readonly userBaseUrl = 'https://localhost:44340/api'
  formData: CardDetail = new CardDetail();
  list: CardDetail[];

  postCardDetails() {
    return this.http.post(this.baseUrl, this.formData);
  }

  public refreshList() {
    this.http.get(this.baseUrl)
    .toPromise()
    .then(res => this.list = res as CardDetail[]);
  }

  putCardDetails() {
    return this.http.put(`${this.baseUrl}/${this.formData.id}`, this.formData);
  }

  deleteCardDetails(id: number)
  {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }




  //User service 

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FullName: [''],
    Password: ['', [Validators.required, Validators.minLength(4)]]
    });


  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Password
    };
    return this.http.post(this.userBaseUrl + '/User/Register', body);
  }

  login(Data: any) {
    return this.http.post(this.userBaseUrl + '/User/Login', Data);
  }

  getUserProfile() {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer ' + localStorage.getItem('token')});
    return this.http.get(this.userBaseUrl + '/userprofile', {headers: tokenHeader});
  }
}