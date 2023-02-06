using StudentManager.Areas.Identity.Data;

namespace StudentManager.Repository
{
	public abstract class RepositoryBase
	{
		protected StudentManagerContext DataContext;
		protected RepositoryBase(StudentManagerContext dataContext)
		{
			DataContext = dataContext;
		}
	}
}
