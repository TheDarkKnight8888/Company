import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DummyRoutingModule } from './dummy-routing.module';
import { DummyComponent } from './dummy.component';

@NgModule({
    declarations: [DummyComponent],
    imports: [CommonModule, DummyRoutingModule]
})
export class DummyModule {}