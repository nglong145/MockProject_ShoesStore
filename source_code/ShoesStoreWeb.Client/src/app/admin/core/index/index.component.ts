import { Component } from '@angular/core';
import { AdminheaderComponent } from '../adminheader/adminheader.component';
import { AdminLayoutComponent } from '../../../Layout/admin-layout/admin-layout.component';

@Component({
  selector: 'app-index',
  imports: [AdminheaderComponent],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css'
})
export class IndexComponent {
  
}
