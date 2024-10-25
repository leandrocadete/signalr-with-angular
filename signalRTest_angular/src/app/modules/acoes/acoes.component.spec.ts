import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcoesComponent } from './acoes.component';
import { AcoesService } from './service/acoes.service';

describe('AcoesComponent', () => {
  let component: AcoesComponent;
  let fixture: ComponentFixture<AcoesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AcoesComponent ],
      providers: [ AcoesService]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
