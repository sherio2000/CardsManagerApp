import { Injectable } from '@angular/core';
import { CardDetail } from './card-detail.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CardDetailService {

  constructor(private http: HttpClient) { }

  readonly baseUrl = 'https://localhost:44340/api/CardDetails'
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
}