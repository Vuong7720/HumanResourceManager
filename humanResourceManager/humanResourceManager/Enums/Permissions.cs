namespace humanResourceManager.Enums
{
	public class Permissions
	{
		// hợp đồng
		public const string ContractManagement = "ContractManagement";
		public const string ContractManagement_Create = "ContractManagement_Create";
		public const string ContractManagement_Update = "ContractManagement_Update";
		public const string ContractManagement_Delete = "ContractManagement_Delete";
		public const string ContractManagement_Approved = "ContractManagement_Approved";

		// Phòng ban
		public const string DepartmentManagement = "DepartmentManagement";
		public const string DepartmentManagement_Create = "DepartmentManagement_Create";
		public const string DepartmentManagement_Update = "DepartmentManagement_Update";
		public const string DepartmentManagement_Delete = "DepartmentManagement_Delete";
		public const string DepartmentManagement_Approved = "DepartmentManagement_Approved";

		// nhân viên
		public const string EmployeeManagement = "EmployeeManagement";
		public const string EmployeeManagement_Create = "EmployeeManagement_Create";
		public const string EmployeeManagement_Update = "EmployeeManagement_Update";
		public const string EmployeeManagement_Delete = "EmployeeManagement_Delete";
		public const string EmployeeManagement_Approved = "EmployeeManagement_Approved";

		// chức vụ
		public const string PositionManagement = "PositionManagement";
		public const string PositionManagement_Create = "PositionManagement_Create";
		public const string PositionManagement_Update = "PositionManagement_Update";
		public const string PositionManagement_Delete = "PositionManagement_Delete";
		public const string PositionManagement_Approved = "PositionManagement_Approved";

		// người dùng
		public const string UserManagement = "UserManagement";
		public const string UserManagement_Create = "UserManagement_Create";
		public const string UserManagement_Update = "UserManagement_Update";
		public const string UserManagement_Delete = "UserManagement_Delete";
		public const string UserManagement_Approved = "UserManagement_Approved";

		/// <summary>
		/// Trả về tất cả permission
		/// </summary>
		public static List<string> All => new()
	{
		ContractManagement, ContractManagement_Create, ContractManagement_Update,
		ContractManagement_Delete, ContractManagement_Approved,

		DepartmentManagement, DepartmentManagement_Create, DepartmentManagement_Update,
		DepartmentManagement_Delete, DepartmentManagement_Approved,

		EmployeeManagement, EmployeeManagement_Create, EmployeeManagement_Update,
		EmployeeManagement_Delete, EmployeeManagement_Approved,

		PositionManagement, PositionManagement_Create, PositionManagement_Update,
		PositionManagement_Delete, PositionManagement_Approved,

		UserManagement, UserManagement_Create, UserManagement_Update,
		UserManagement_Delete, UserManagement_Approved
	};

	}


}
