using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace FormatPendingChanges.DocumentActions
{
    [Export(typeof(DocumentAction))]
    internal sealed class PythonFormatDocumentAction : FormatDocumentAction
    {
        private static readonly string[] PythonFileExtensions = { ".py" };

        [ImportingConstructor]
        public PythonFormatDocumentAction(SVsServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override bool CanExecute(ProjectItem projectItem)
        {
            return projectItem != null && PythonFileExtensions.Any(p => projectItem.Name.EndsWith(p, StringComparison.OrdinalIgnoreCase));
        }
    }
}
