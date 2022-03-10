import { Component, OnInit } from '@angular/core';
import { ConfirmBoxEvokeService, IConfirmBoxPublicResponse } from '@costlydeveloper/ngx-awesome-popup';
import { ToastrService } from 'ngx-toastr';
import { CardDetail } from '../shared/card-detail.model';
import { CardDetailService } from '../shared/card-detail.service';

@Component({
  selector: 'app-card-details',
  templateUrl: './card-details.component.html',
  styles: [
  ]
})
export class CardDetailsComponent implements OnInit {

  constructor(public service: CardDetailService, private toastr: ToastrService, private confirmBoxEvokeService: ConfirmBoxEvokeService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: CardDetail)
  {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number, response: IConfirmBoxPublicResponse)
  {
    console.log(id);
    if(response.success)
    {
      if(id.toString() == "0")
      {
        this.toastr.error("Please select card first!", "Error");
      } else {
        this.service.deleteCardDetails(id)
        .subscribe(
          res =>{
            this.service.refreshList();
            this.toastr.success("Card Deleted!", "Success");
          },
            err => {
            this.toastr.error("There seems to be a problem!", "Error");
          }
        )
      }
    }
    
  }
  confirmBox(id: number) {
    this.confirmBoxEvokeService
      .danger('Warning', 'Are you sure you want to delete this card?', 'Confirm', 'Cancel')
      .subscribe((resp) => this.onDelete(id, resp));
  }

}
