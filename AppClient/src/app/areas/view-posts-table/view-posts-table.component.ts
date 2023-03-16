import { Component, OnInit,Input, SimpleChanges, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Post } from '../../models/post';



@Component({
  selector: 'app-view-posts-table',
  templateUrl: './view-posts-table.component.html',
  styleUrls: ['./view-posts-table.component.css']
})
export class ViewPostsTableComponent implements OnInit {

  @Input() table_data: Post[] = [];
  displayedColumns: string[] = ['id', 'author', 'authorId', 'likes', 'popularity', 'reads', 'tags'];
  dataSource: MatTableDataSource<Post> = new MatTableDataSource<Post>([]);

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  pageSizeOptions = [20, 10, 5];

  constructor() { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.table_data);
    this.dataSource.paginator = this.paginator;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes.table_data.currentValue){
      this.dataSource.data = changes.table_data.currentValue;
    }
  }
}
