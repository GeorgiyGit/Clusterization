import { Component, OnInit, ViewChild } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { YoutubeChannelService } from '../../services/youtube-channel.service';
import { ISimpleChannel } from '../../models/simple-channel';
import { Router } from '@angular/router';
import { YoutubeChannelListComponent } from '../youtube-channel-list/youtube-channel-list.component';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-youtube-load-multiple-channels',
  templateUrl: './youtube-load-multiple-channels.component.html',
  styleUrls: ['./youtube-load-multiple-channels.component.scss']
})
export class YoutubeLoadMultipleChannelsComponent {
  value: string = '';
  nextPageToken: string | undefined;

  channels: ISimpleChannel[] = [];
  selectedChannels: ISimpleChannel[] = [];

  filterType:string='Date';

  options:IOptionForSelectInput[]=[
    {
      value:'Rating',
      description:'Спочатку популярніші'
    },
    {
      value:'Date',
      description:'Спочатку новіші'
    }
  ]

  constructor(private toastrService: MyToastrService,
    private channelService: YoutubeChannelService,
    private router: Router) { }


  @ViewChild(YoutubeChannelListComponent) filterChild: YoutubeChannelListComponent;
  changeValue(event: any) {
    this.value = event.target.value;
    this.nextPageToken = undefined;
  }

  isLoading: boolean = false;
  loadFirst() {
    if (this.value == null || this.value == '') {
      this.toastrService.error('Поле для назви пусте!!!');
      return;
    }
    this.nextPageToken=undefined;

    this.isLoading = true;
    this.channelService.getWithoutLoading(this.value, this.nextPageToken,this.filterType).subscribe(res => {
      this.selectedChannels=[];
      
      res.channels.forEach(elem=>{
        elem.isSelectAvailable=true;
      });

      this.channels = res.channels.reverse();
      this.nextPageToken = res.nextPageToken;
      this.isLoading = false;
    },
      error => {
        this.isLoading = false;
        this.toastrService.error(error.error.Message);
      });
  }

  isLoading2:boolean;
  loadMore() {
    if (this.value == null || this.value == '') {
      this.toastrService.error('Поле для назви пусте!!!');
      return;
    }
    if (this.nextPageToken == undefined) return;

    this.isLoading2 = true;
    this.channelService.getWithoutLoading(this.value, this.nextPageToken,this.filterType).subscribe(res => {
      res.channels.forEach(elem=>{
        elem.isSelectAvailable=true;
      });

      this.channels = this.channels.concat(res.channels.reverse());
      this.nextPageToken = res.nextPageToken;
      this.isLoading2 = false;
    },
      error => {
        this.isLoading2 = false;
        this.toastrService.error(error.error.Message);
      });
  }

  selectChannel(channel: ISimpleChannel) {
    if (this.selectedChannels.find(c => c == channel) == null) this.selectedChannels.push(channel);
  }
  unselectChannel(channel: ISimpleChannel) {
    this.selectedChannels = this.selectedChannels.filter(c => c != channel);
  }

  load() {
    if (this.selectedChannels == null || this.selectedChannels.length == 0) {
      this.toastrService.error('Каналів не вибрано');
      return;
    }

    let ids: string[] = [];
    this.selectedChannels.forEach(elem => {
      ids.push(elem.id);
    });

    this.isLoading = true;
    this.channelService.loadManyByIds(ids).subscribe(res => {
      this.channels = [];
      this.selectedChannels = [];
      this.value = '';
      this.nextPageToken = undefined;

      this.toastrService.success('Канали завантажено');
      this.isLoading = false;
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading = false;
      this.toastrService.error(error.error.Message);
    });
  }

  filterTypeChanges(type:IOptionForSelectInput){
    if(type.value!=null)this.filterType=type.value;

    if(this.value==null || this.value=='')return;

    this.loadFirst();
  }
}
