export interface ResponseResult {
    message: string;
    error: string;
    data: any;
    dataTotal: any;
    totalRecord: number;
    metadata?: any;
    totalCount?: number;
    status?: number;
    success?: boolean;
}

export interface PermissionDto {
    id: number;
    permissionName?: string;
    creationTime?: string; 
  }

  export interface CreateUpdateRoleDto {
    roleName?: string;
    permissionIds: number[];
    userName?: string;
  }
  

  export interface RoleDto {
    id: number;
    roleName?: string;
    permissionIds: number[];
    permissionNames: string[];
    isDeleted: boolean;
    isStatic: boolean;
    creationName?: string;
    creationTime?: string;
    updatedBy?: string;
    updatedAt?: string;
  }
  

  export interface UsersDto {
    id: number;
    employeeID?: number; // nullable FK
    // employee?: EmployeesDto; // You should define this separately if needed
  
    username?: string;
    password?: string;
  
    // role: RoleDto; // assuming `Role` is the same as `RoleDto`
    roleIds: number[];
    roleNames: string[];
  
    permissionIds: number[];
    permissionNames: string[];
  
    isDeleted: boolean;
    isStatic: boolean;
  
    creationName?: string;
    creationTime?: string;
    updatedBy?: string;
    updatedAt?: string;
  }

  export interface SelectOptionItems {
    value: number;
    label: string;
  }