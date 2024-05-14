import { AccountService } from './../_services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { SearchUserComponent } from '../modals/search-user/search-user.component';
import { UserParams } from '../_models/userParams';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  showSearch: boolean = false;
  bsModalRef: BsModalRef;

  constructor(public accountService: AccountService, private router: Router, 
    private toastr: ToastrService, private modalService: BsModalService) { }  // dua accountservice, router va toastrservice vao ham tao

  ngOnInit(): void {
  }

  login() { // phuong thuc dung accountservice va login de thuc hien dang nhap va truyen vao doi tuong model
    this.accountService.login(this.model).subscribe(response => {  //phuong thuc duoc goi tra ve, cho phep nhan thong bao khi co phan hoi
      this.router.navigateByUrl('/members'); //dieu huong router url gui toi /members
    });
  }

  logout() {
    this.accountService.logout();   //sau khi dang xuat la 1 danh sach dinh tuyen su dung dieu huong url
    this.router.navigateByUrl('/'); //kiem tra hoat dong dang nhap vao va dang xuat ra de kiem tra 
  }

  toggleSearch() {
    this.bsModalRef =this.modalService.show(SearchUserComponent)
  }
  
}
