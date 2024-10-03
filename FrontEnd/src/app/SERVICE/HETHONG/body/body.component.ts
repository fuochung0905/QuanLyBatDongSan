import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-body',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './body.component.html',
  styleUrl: './body.component.css'
})
export class BodyComponent {

  @Input() collapsed = false;
  @Input() scressWidth = 0;
  getBodyClass(): string{
    let styleClass = '';
    if(this.collapsed && this.scressWidth > 100){
      styleClass = 'body-trimmed';
    }else if(this.collapsed && this.scressWidth <=768 && this.scressWidth >=0){
      styleClass = 'body-md-screen'
    }
    return styleClass;
  }
}
