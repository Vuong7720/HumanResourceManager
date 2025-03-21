import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-humanResource',
  templateUrl: './humanResource.component.html',
  styleUrls: ['./humanResource.component.scss']
})
export class HumanResourceComponent implements OnInit {
  data: ItemData[] = [];
  listOfColumn = [
    {
      title: 'Tên nhân viên',
      compare: (a: ItemData, b: ItemData) => a.name.localeCompare(b.name),
      priority: false
    },
    {
      title: 'Tuổi',
      compare: (a: ItemData, b: ItemData) => a.age - b.age,
      priority: 4
    },
    {
      title: 'Giới tính',
      compare: (a: ItemData, b: ItemData) =>  a.gender.localeCompare(b.gender),
      priority: 3
    },
    {
      title: 'Chức vụ',
      compare: (a: ItemData, b: ItemData) =>  a.position.localeCompare(b.position),
      priority: 2
    },
    {
      title: 'Phòng ban',
      compare: (a: ItemData, b: ItemData) =>  a.department.localeCompare(b.department),
      priority: 1
    }
  ];
  listOfData: ItemData[] = [
    {
      name: 'Nguyễn Đình Tân',
      age: 24,
      gender: 'Nữ',
      position: 'Thư ký',
      department: 'Kế toán'
    },
    {
      name: 'Nguyễn Quang Vũ',
      age: 22,
      gender: 'Bê đê',
      position: 'Thư ký',
      department: 'Maketting'
    },
    {
      name: 'Lê Văn Văn',
      age: 26,
      gender: 'Nữ',
      position: 'Thư ký',
      department: 'Thư ký giám đốc'
    },
    {
      name: 'Vũ Lâm Phương',
      age: 20,
      gender: 'Nữa nạc nữa mỡ',
      position: 'Tiếp tân',
      department: 'Tiếp tân'
    }
  ];

  ngOnInit(): void {

  }




}

interface ItemData {
  name: string;
  age: number;
  gender: string;
  position: string;
  department: string;
}