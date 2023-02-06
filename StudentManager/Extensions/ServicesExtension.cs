
using StudentManager.Core;
using StudentManager.Core.Repository;
using StudentManager.Repository;

namespace StudentManager.Extensions
{
	public static class ServicesExtension
	{
		public static void AddAuthorizationPolicies(this IServiceCollection services)
		{
			services.AddAuthorization(options =>
			{
				options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("Employee"));
				options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
				options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));
			});
		}

		public static void RegisterRepositoryScoped(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IGradeRepository, GradeRepository>();
			services.AddScoped<IStudentRepository, StudentRepository>();
			services.AddScoped<IRepositories, Repositories>();
		}

	}
}
