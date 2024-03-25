import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Newfeed } from '../_models/newfeed';
import { NewfeedService } from '../_services/newfeed.service';
import { ToastrService } from 'ngx-toastr';
import { PresenceService } from '../_services/presence.service';
import { Pagination } from '../_models/pagination';
import { ConfirmService } from '../_services/confirm.service';
import { FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-newfeed',
  templateUrl: './newfeed.component.html',
  styleUrls: ['./newfeed.component.css']
})
export class NewfeedComponent implements OnInit {

  @ViewChild('editForm') editForm: NgForm;
  newfeeds: Newfeed[] | undefined = [];
  pagination: Pagination | undefined;
  container = 'Outbox';
  pageNumber = 1;
  pageSize = 20;
  loading = false;
  newfeedForm: FormGroup;
  validationErrors: string[] = [];

  ngOnInit(): void {
    this.loadNewfeed();
  }
  constructor(private newfeedService: NewfeedService, private toastr: ToastrService,
    private confirmService: ConfirmService, private router: Router) {  //

  }

  loadNewfeed() {
    this.loading = true;
    this.newfeedService.getAllNewfeed(this.pageNumber, this.pageSize, this.container).subscribe(response => {
      this.newfeeds = response.result;
      this.pagination = response.pagination;
      this.loading = false;
    })
  }

  addNewfeed() {
    this.newfeedService.addNewfeed(this.newfeedForm.value).subscribe(response => {
      this.router.navigateByUrl('/newfeed');
    }, error => {
      this.validationErrors = error;
    })
  }

  updateNewfeed() {
    this.newfeedService.updateNewFeed(this.newfeedForm.value).subscribe(() =>{
      this.toastr.success('Updated successfully!!!');
      this.editForm.reset(this.newfeeds);
    })
   }

  delete(id: number) {
    this.confirmService.confirm('Confirm delete this post?', 'this cannot be undone').subscribe(result => {
      if (result) {
        this.newfeedService.deleteNewfeed(id).subscribe(() => {
          this.newfeeds?.splice(this.newfeeds.findIndex(m => m.id === id), 1);
        })
      }
    })
  }
}
