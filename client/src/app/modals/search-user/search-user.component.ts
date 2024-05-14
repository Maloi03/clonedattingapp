import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { MemberService } from 'src/app/_services/member.service';


@Component({
  selector: 'app-search-user',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent implements OnInit {

  @Output() Search = new EventEmitter();
  user: User;
  roles: any[];
  result: boolean;

  members: Member[] = [];
  pagination: Pagination | undefined;
  userParams: UserParams | undefined ;
  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }]

  constructor(public bsModalRef: BsModalRef,private memberService: MemberService)
   { this.userParams = this.memberService.getUserParams(); }

  ngOnInit(): void {
    
  }

  confirm(){
    this.result = true; 
    this.Search.emit(this.userParams);
     this.bsModalRef.hide();
  }

   resetFilters() {
     this.userParams = this.memberService.resetUserParams();
    //  this.loadMembers();
     this.result = false; 
     //this.bsModalRef.hide();
   }


}