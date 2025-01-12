import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-review',
  imports: [CommonModule, FormsModule],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css',
})
export class ReviewComponent {
  reviews = [
    {
      user: 'Nguyen Van A',
      date: '01-01-2024 00:00',
      rating: 5,
      comment: 'Ngon, bổ, rẻ',
    },
    {
      user: 'Nguyen Van B',
      date: '02-01-2024 00:01',
      rating: 4,
      comment: 'Tạm',
    },
  ];
  userRating = 0;
  userComment = '';
  showLoadMore = true;

  setRating(rating: number) {
    this.userRating = rating;
  }

  submitReview() {
    if (this.userComment.trim() && this.userRating > 0) {
      this.reviews.unshift({
        user: 'Trần Thanh C',
        date: new Date().toLocaleString(),
        rating: this.userRating,
        comment: this.userComment,
      });
      this.userRating = 0;
      this.userComment = '';
    }
  }

  loadMore() {
    // Thêm logic tải thêm đánh giá nếu cần
    this.showLoadMore = false;
  }
}
