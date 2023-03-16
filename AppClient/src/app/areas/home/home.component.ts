import { PostService } from "./../../services/post.service";
import { Component, OnInit } from '@angular/core';
import { Subscription } from "rxjs";
import { Post } from "../../models/post";
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  tags: string = '';
  sortBy: string = 'id';
  direction: string = 'asc';
  posts: Post[] = [];

  private subscriptions: Subscription[]= [];

  constructor(private postSevice: PostService,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(x => x.unsubscribe());
  }

  showTable() {
    const tagsArr = this.getTagsFromInput();

    const subscription = this.postSevice.getPosts(tagsArr, this.sortBy, this.direction).subscribe(
      posts => {
        this.posts = posts;
        this.snackBar.open(`Received ${this.posts.length} posts.`, 'Dismiss', {
          duration: 3000, // Notification duration in milliseconds
          panelClass: ['mat-snack-bar-container--success'], // CSS class for the notification container
        });
      },
      error => {
        console.log(error);
        this.snackBar.open('Error occurred!', 'Dismiss', {
          duration: 3000, // Notification duration in milliseconds
          panelClass: ['mat-snack-bar-container--error'], // CSS class for the notification container
        });
      }
    )

    this.subscriptions.push(subscription);
  }

  isValidTag(): boolean {
    return !!this.tags && this.tags.trim().length > 0;
  }

  private getTagsFromInput(): string[] {
    const tagsArray = this.tags.split(",").map(tag => tag.trim());
    return tagsArray;
  }

}
