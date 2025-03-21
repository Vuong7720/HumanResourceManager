import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  data: ItemData[] = [];

  ngOnInit(): void {
    
  }

  

}

interface ItemData {
  href: string;
  title: string;
  avatar: string;
  description: string;
  content: string;
}