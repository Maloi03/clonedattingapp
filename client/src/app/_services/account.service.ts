import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { PresenceService } from './presence.service';
import { ToastrService } from 'ngx-toastr';


@Injectable({  // big data duoc cung cap
  providedIn: 'root'
})
export class AccountService {
  [x: string]: any;
   baseUrl = environment.apiUrl;  //gan link API co so tu environment de cung cap dich vu cho tai khoan
   private currenUserSource = new ReplaySubject<User>(1)
   currentUser$ = this.currenUserSource.asObservable();

  constructor(private http: HttpClient, private presence: PresenceService, private toastr:ToastrService) { } // dua httpclient vao de nhan duoc dich vu

  login(model: any) { // tao phuong thuc login, dat model nguoi dung de nen du lieu vao ben trong va cho no gia tri any
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe( //them user vao trong post de dan tra ve dung tai khoan nguoi dung sau khi dang nhap
      map((reponse: User) => {
         const user = reponse;
         if (user) {
           this.setCurrentUser(user);
           this.presence.createHubConnection(user);
         }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
         if (user) {
          this.setCurrentUser(user);
          this.presence.createHubConnection(user);
         }
      })
    )
  }

    setCurrentUser(user: User) {
      user.roles = [];
      const roles = this.getDecodedToken(user.token).role;
      Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
      localStorage.setItem('user', JSON.stringify(user));
      this.currenUserSource.next(user);
    }

    logout() {
      localStorage.removeItem('user');  //luu tru cuc bo du lieu nguoi dung khi dang xuat, khong xoa muc va su dung khoa nguoi dung
      this.currenUserSource.next(null!);
      this.presence.stopHubConnection();
    }

    getDecodedToken(token: any) {
      return JSON.parse(atob(token.split('.')[1]));
    }
}
