import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { Newfeed } from '../_models/newfeed';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';


@Injectable({
  providedIn: 'root'
})
export class NewfeedService {
  baseUrl = environment.apiUrl;
  newfeeds: Newfeed[] = [];

  constructor(private http: HttpClient) { }
  
  getAllNewfeed(pageNumber: number, pageSize: number, container: string) {//neu hien loi khai bao bien
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Newfeed[]>(this.baseUrl + 'newfeed', params, this.http);
  }

  addNewfeed(model:any){ //////////////////////////////////////////sua sau
    return this.http.post(this.baseUrl + 'newfeed/' , model);
  }

  updateNewFeed(postId: number){
    return this.http.put(this.baseUrl + 'newfeed/' + postId, {});
  }

  deleteNewfeed(postId: number){
    return this.http.delete(this.baseUrl + 'newfeed/' + postId, {});
  }
}
