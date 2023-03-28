import { map } from 'rxjs/operators';
import { Member } from './../_models/member';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class MemberService {
  baseUrl = environment.apiUrl;
  members : Member [] = [];

  constructor(private http: HttpClient) { }

  getMembers() {
    if (this.members.length > 0) return of (this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members => {
         this.members = members;
         return members;
      })
    )
  }

  getMember(username: string) {
     const member = this.members.find(x => x.username === username);
     if(member !== undefined) return of (member);
     return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

    updateMember(_member :Member){
        return this.http.put(this.baseUrl + 'users', _member)
        .pipe(
           map(() =>   {
          const index = this.members.indexOf(_member);
          this.members[index] = _member;
        })  
      )
    }
    setMainPhoto(photoId: number){
      return this.http.put(this.baseUrl + 'users/set-main-photo/'+ photoId, {});
    }
    deletePhoto(photoId: number){
      return  this.http.delete(this.baseUrl +'users/delete-photo/' + photoId);
    }
}
