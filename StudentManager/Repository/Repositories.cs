using StudentManager.Core.Repository;

namespace StudentManager.Repository
{
	public class Repositories : IRepositories
	{
		public IUserRepository UserRepository { get; }
		public IRoleRepository RoleRepository { get; }
		public IGradeRepository GradeRepository { get; }
		public IStudentRepository StudentRepository { get; }
		public Repositories(
			IUserRepository userRepository,
			IRoleRepository roleRepository,
			IGradeRepository gradeRepository,
			IStudentRepository studentRepository)
		{
			UserRepository = userRepository;
			RoleRepository = roleRepository;
			GradeRepository = gradeRepository;
			StudentRepository = studentRepository;
		}
	}
}
