import { User } from './../_models/user';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  showLoginForm: boolean = false;

  toggleLoginForm() {
    this.showLoginForm = !this.showLoginForm;}
  
   registerMode = false;
   model: any = {};
  constructor(public accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
  }
  

  registerToggle() {
     this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  login() { // phuong thuc dung accountservice va login de thuc hien dang nhap va truyen vao doi tuong model
    this.accountService.login(this.model).subscribe(response => {  //phuong thuc duoc goi tra ve, cho phep nhan thong bao khi co phan hoi
      this.router.navigateByUrl('/members'); //dieu huong router url gui toi /members
    });
  }
}
