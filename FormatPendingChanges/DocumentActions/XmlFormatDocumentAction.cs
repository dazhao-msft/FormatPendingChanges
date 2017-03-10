using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace FormatPendingChanges.DocumentActions
{
    [Export(typeof(DocumentAction))]
    internal sealed class XmlFormatDocumentAction : FormatDocumentAction
    {
        private static readonly string[] XmlBasedFileExtensions = { ".xml", ".vsixmanifest", ".vstemplate", ".vsct", ".props", ".targets", ".wxs", ".wxl", ".wxi" };

        private static readonly Guid RoslyProjectKind = new Guid("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}");

        [ImportingConstructor]
        public XmlFormatDocumentAction(SVsServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override bool CanExecute(ProjectItem projectItem)
        {
            if (projectItem == null)
            {
                return false;
            }

            if (projectItem.ContainingProject == null)
            {
                return false;
            }

            if (projectItem.ContainingProject.Kind == null)
            {
                return false;
            }

            if (new Guid(projectItem.ContainingProject.Kind) == RoslyProjectKind)
            {
                //
                // Edit.FormatDocument doesn't work with the XML editor in the Rosly project system. Disable for now.
                //

                return false;
            }

            return XmlBasedFileExtensions.Any(p => projectItem.Name.EndsWith(p, StringComparison.OrdinalIgnoreCase));
        }
    }
}
