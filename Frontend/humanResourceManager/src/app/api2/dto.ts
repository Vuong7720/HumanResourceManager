

export function checkHasProp<T>(object: T, key: string | keyof T): key is keyof T {
  return Object.prototype.hasOwnProperty.call(object, key);
}


export class ListResultDto<T> {
  items?: T[];

  constructor(initialValues: Partial<ListResultDto<T>> = {}) {
    for (const key in initialValues) {
      if (checkHasProp(initialValues, key)) {
        this[key] = initialValues[key];
      }
    }
  }
}

type ValueOf<T> = T[keyof T];
export class PagedResultDto<T> extends ListResultDto<T> {
  totalCount?: number;

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