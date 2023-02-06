namespace StudentManager.Core.Repository
{
	public interface IRepositories
	{
		IUserRepository UserRepository { get; }
		IRoleRepository RoleRepository { get; }
		IGradeRepository GradeRepository { get; }
		IStudentRepository StudentRepository { get; }
	}
}
