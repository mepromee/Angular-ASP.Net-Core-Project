import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from '../models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private httpClient: HttpClient) { }

  private apiUrl = 'https://localhost:7038/api/posts';

  getPosts(tags: string[], sortBy: string, direction: string): Observable<Post[]> {
    let params = new HttpParams();
    tags.forEach(tag => {
      params = params.append('tags', tag);
    });
    params = params.append('sortBy', sortBy);
    params = params.append('direction', direction);

    return this.httpClient.get<Post[]>(this.apiUrl, { params });
  }
}
