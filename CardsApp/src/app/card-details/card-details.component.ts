import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmBoxEvokeService, IConfirmBoxPublicResponse } from '@costlydeveloper/ngx-awesome-popup';
import { ToastrService } from 'ngx-toastr';
import { CardDetail } from '../shared/card-detail.model';
import { CardDetailService } from '../shared/card-detail.service';
import { UserModel } from '../shared/user-model';

@Component({
  selector: 'app-card-details',
  templateUrl: './card-details.component.html',
  styles: [
  ]
})
export class CardDetailsComponent implements OnInit {

  constructor(public service: CardDetailService, private toastr: ToastrService, private confirmBoxEvokeService: ConfirmBoxEvokeService, private router: Router) { }

      
  userDetails: any;

  ngOnInit(): void {
    this.service.refreshList();
    // this.service.getUserProfile().subscribe(
    //   res => {
    //     this.userDetails = res;
    //   },
    //   err => {
    //     console.log(err);
    //   },
    // );
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

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }

  confirmBox(id: number) {
    this.confirmBoxEvokeService
      .danger('Warning', 'Are you sure you want to delete this card?', 'Confirm', 'Cancel')
      .subscribe((resp) => this.onDelete(id, resp));
  }

}
