import { Component, OnInit } from '@angular/core';
import { Form, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CardDetailService } from 'src/app/shared/card-detail.service';
import { map } from 'rxjs/operators';
import { CardDetail } from 'src/app/shared/card-detail.model';



@Component({
  selector: 'app-card-details-form',
  templateUrl: './card-details-form.component.html',
  styles: [
  ]
})
export class CardDetailsFormComponent implements OnInit {

  constructor(public service: CardDetailService, public toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm)
  {
    if(this.service.formData.id == 0)
    {
      this.insertRecord(form);
    }
    else{
      this.updateRecord(form);
    }
      
    
  }

  insertRecord(form: NgForm)
  {
    this.service.postCardDetails().subscribe(
      res => {
        this.toastr.success("Card Added!", "Success");
        this.resetForm(form);
        this.service.refreshList();
      },
      () => {
        this.toastr.error("There seems to be a problem!", "Error");
      }
    );
  }

  updateRecord(form: NgForm)
  {
    this.service.putCardDetails().subscribe(
      res => {
        this.toastr.info("Card Updated!", "Success");
        this.resetForm(form);
        this.service.refreshList();
      },
      err => {
        this.toastr.error("There seems to be a problem!", "Error");
      }
    );
  }

  resetForm(form: NgForm)
  {
    form.form.reset();
    this.service.formData = new CardDetail();
  }

}
