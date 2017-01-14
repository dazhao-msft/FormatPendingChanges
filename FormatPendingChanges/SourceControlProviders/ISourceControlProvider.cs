using EnvDTE;
using System.Threading.Tasks;

namespace FormatPendingChanges.SourceControlProviders
{
    internal interface ISourceControlProvider
    {
        Task<bool> ContainsSolutionAsync(Solution solution);

        Task<string[]> QueryPendingChangesAsync(Solution solution);

        Task UndoUnmodifiedChangesAsync(Solution solution);
    }
}
