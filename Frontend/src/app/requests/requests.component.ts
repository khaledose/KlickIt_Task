import { Component, OnInit } from '@angular/core';
import { RequestsService } from '../_services/requests.service';
import { TokenStorageService } from '../_services/token-storage.service';
import { UserStateService } from '../_services/user-state.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {
  requests: any = [];
  requestStatus: { [name: number]: string } = {0:'Pending', 1:'Processing', 2:'Completed'};
  showMore: boolean = false;
  pageSize: number = 5;
  page: number = 1;
  userId: any = null;
  constructor(public requestsService:RequestsService, public userStateService:UserStateService, private tokenStorageService:TokenStorageService) { }

  ngOnInit(): void {
    if(this.userStateService.state.isUser){
      this.userId = this.tokenStorageService.getUser().userId;
    }
    this.requestsService.getRequests(this.userId, this.page).subscribe({
      next: data => {
        console.log(data);
        this.requests = data.entities;
        this.showMore = data.count > this.pageSize * this.page;
      },
      error: err => {

      }
    });
  }

  deleteRequest(id: any): void{
    this.requestsService.deleteRequest(id.value).subscribe({
      next: data => {
        window.location.reload();
      },
      error: err => {

      }
    });
  }

  updateStatus(id: any, status: number): void{
    this.requestsService.updateStatus(id.value, status).subscribe({
      next: data => {
        window.location.reload();
      },
      error: err => {

      }
    });
  }

  loadMore(): void{
    this.page = Math.ceil(this.requests.length / this.pageSize) + 1;
    console.log(this.page);
    this.requestsService.getRequests(this.userId, this.page).subscribe({
      next: data => {
        this.requests = this.requests.concat(data.entities);
        this.showMore = data.count > this.pageSize * this.page;
      },
      error: err => {

      }
    });
  }
}
