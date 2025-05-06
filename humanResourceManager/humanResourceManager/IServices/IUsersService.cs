using humanResourceManager.Models;
using humanResourceManager.Models.UsersModel;

namespace humanResourceManager.IServices
{
	public interface IUsersService
	{
		Task<UsersDto> CreateAsync(CreateUpdateUsersDto input);

		Task<UsersDto> UpdateAsync(int id, CreateUpdateUsersDto input);

		Task<PagedResultDto<UsersDto>> GetPagingDto(PagingRequest request);
		Task<UsersDto> GetById(int id);

		Task DeleteAsync(int id);

		Task DeleteMultipleAsync(IEnumerable<int> ids);

		Task<UsersDto> SignIn(SignInInfo input);


        #region login logout
        Task<string> LoginAsync(LoginDto dto);
		Task<MessageDto> RegisterAsync(RegisterDto dto);
        #endregion
    }
}
