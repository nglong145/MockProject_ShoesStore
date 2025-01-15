import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrandViewModel } from '../model/brand_viewmodel';
import { BrandService } from '../service/brand-service.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-update-brand',
  imports: [FormsModule],
  templateUrl: './update-brand.component.html',
  styleUrl: './update-brand.component.css'
})
export class UpdateBrandComponent {
  brand: BrandViewModel
  id?: string | null
  fileImage: File | null = null
  baseUrl: string = "https://localhost:7158/Images/Product/";

  activedRouteSubscription?: Subscription
  updateBrandSubscripton?: Subscription

  constructor(private brandService: BrandService, private activedRoute: ActivatedRoute){
    this.brand = {
      brandName: '',
      brandImage: '',
      description: ''
    }
  }
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.brand.brandImage = this.baseUrl + input.files[0].name; // Lấy tên file
      this.fileImage = input.files[0];
    } else {
      this.brand.brandImage = ''; // Không có file được chọn
    }
  }
  ngOnInit() {
    this.activedRouteSubscription = this.activedRoute.paramMap.subscribe({
      next: params => {
        this.id = params.get('id');
        if(this.id){
          this.brandService.getBrandById(this.id).subscribe({
            next: reponse =>{
              this.brand = reponse
            },
            error: error => {
              console.log("error", error)
            }
          })
        }
      }
    })
  }
  ngOnDestroy(){
    this.activedRouteSubscription?.unsubscribe
    this.updateBrandSubscripton?.unsubscribe
  }

  updateBrand(){
    if(this.id){
      if(this.brand){
        this.updateBrandSubscripton = this.brandService.updateBrand(this.id, this.brand).subscribe({
          next: reponse => {
            console.log("update ok")
          },
          error: err => {
            console.log("update error")
          }
        })
      }
    }
  }
}
