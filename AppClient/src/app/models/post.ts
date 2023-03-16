export interface Post {
  id: number;
  author: string;
  authorId: number;
  likes: number;
  popularity: number;
  reads: number;
  tags: string[];
}
