

export function checkHasProp<T>(object: T, key: string | keyof T): key is keyof T {
  return Object.prototype.hasOwnProperty.call(object, key);
}


export class ListResultDto<T> {
  items: T[]; // bỏ dấu "?"

  constructor(initialValues: Partial<ListResultDto<T>> = {}) {
    this.items = initialValues.items ?? []; // gán mặc định []
  }
}


type ValueOf<T> = T[keyof T];
export class PagedResultDto<T> extends ListResultDto<T> {
  totalCount: number = 0;

  constructor(initialValues: Partial<PagedResultDto<T>> = {}) {
    super(initialValues);
  }
}


export interface Message {
  Success: boolean;
  Title: string;
  Obj?: any;
  Continue?: boolean;
  Month?: number;
  Year?: number;
}