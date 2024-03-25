import { Component, OnInit } from "@angular/core";
import { Pagination } from "../_models/pagination";
import { MemberService } from "../_services/member.service";
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members: Member[] = [];
  //members: Partial<Member[] | undefined>;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination | undefined;

  constructor(private memberService: MemberService) { this.members = [];}

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.memberService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe({
      next: response => {
        this.members = response.result as [];
        this.pagination = response.pagination;
      }
    })
  }
  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadLikes();
    }
  }

}