import { Component, Input } from '@angular/core';
import { ISimpleExternalObjectsPack } from '../../models/responses/simple-external-objects-pack';
import { Router } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

@Component({
  selector: 'app-external-objects-pack-card',
  templateUrl: './external-objects-pack-card.component.html',
  styleUrl: './external-objects-pack-card.component.scss'
})
export class ExternalObjectsPackCardComponent {
  @Input() pack:ISimpleExternalObjectsPack;
  
  isYour:boolean=false;
  constructor(private router:Router,
    public myLocalStorage: MyLocalStorageService,
    public accountService:AccountService){}

  ngOnInit(): void {
    let id = this.accountService.getUserId();
    if(id!=null && this.pack!=null && id==this.pack.ownerId){
      this.isYour=true;
    }
  }

  openFull(){
    this.router.navigateByUrl('externalData-layout/dataSources/externalData/packs/full/'+this.pack.id+'/list/'+this.pack.id);
  }
}
