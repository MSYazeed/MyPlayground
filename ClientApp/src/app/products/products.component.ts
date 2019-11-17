import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
})
export class ProductsComponent {
  public product: productData[];
  productId: string;

  constructor(private httpClient: HttpClient) {
  }

  send() {
    this.httpClient.get<productData[]>( 'api/products/' + this.productId).subscribe(result => {
      this.product = result;
    }, error => console.error(error));
  }

}

interface productData {
  id: number;
  name: string;
  category: string;
  price: number;
}

