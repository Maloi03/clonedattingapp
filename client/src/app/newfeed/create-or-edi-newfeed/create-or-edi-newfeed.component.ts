import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Newfeed } from 'src/app/_models/newfeed';
import { NewfeedService } from 'src/app/_services/newfeed.service';

@Component({
  selector: 'app-create-or-edi-newfeed',
  templateUrl: './create-or-edi-newfeed.component.html',
  styleUrls: ['./create-or-edi-newfeed.component.css']
})
export class CreateOrEdiNewfeedComponent implements OnInit {
  @Input() newfeed: Newfeed;
  newfeedForm: FormGroup;
  validationErrors: string[] = [];
  @ViewChild('editForm') editForm: NgForm;
 // newfeeds: Newfeed;
  
  constructor(private newfeedService: NewfeedService,private router: Router,private toastr: ToastrService){
    
  }

  ngOnInit(): void {
    
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
      this.editForm.reset(this.newfeed);
    })
   }
}
