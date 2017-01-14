using EnvDTE;
using System.Threading.Tasks;

namespace FormatPendingChanges.DocumentActions
{
    internal abstract class DocumentAction
    {
        public abstract bool CanExecute(ProjectItem projectItem);

        public abstract Task ExecuteAsync(ProjectItem projectItem);
    }
}
