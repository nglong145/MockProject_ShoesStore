import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './User/Core/Component/header/header.component';
import { SliderComponent } from './User/Core/Component/slider/slider.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'ShoesStoreWeb.Client';
}
