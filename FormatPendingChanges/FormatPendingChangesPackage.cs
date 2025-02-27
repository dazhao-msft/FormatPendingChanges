﻿using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace FormatPendingChanges
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
    [Guid(GuidList.guidFormatPendingChangesPkgString)]
    public sealed class FormatPendingChangesPackage : Package
    {
        private FormatPendingChangesService _service;

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public FormatPendingChangesPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", ToString()));
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", ToString()));
            base.Initialize();

            _service = new FormatPendingChangesService(this);

            // Add our command handlers for menu (commands must exist in the .vsct file)
            if (GetService(typeof(IMenuCommandService)) is OleMenuCommandService mcs)
            {
                {
                    var formatPendingChangesCommand = new OleMenuCommand(_service.FormatPendingChanges,
                                                                         null,
                                                                         _service.QueryStatus,
                                                                         new CommandID(GuidList.guidFormatPendingChangesCmdSet, (int)PkgCmdIDList.idFormatPendingChangesCommand));
                    mcs.AddCommand(formatPendingChangesCommand);
                }
                {
                    var formatSolutionCommand = new OleMenuCommand(_service.FormatSolution,
                                                                   null,
                                                                   _service.QueryStatus,
                                                                   new CommandID(GuidList.guidFormatPendingChangesCmdSet, (int)PkgCmdIDList.idFormatSolutionCommand));
                    mcs.AddCommand(formatSolutionCommand);
                }
                {
                    var undoUnmodifiedChangesCommand = new OleMenuCommand(_service.UndoUnmodifiedChanges,
                                                                          null,
                                                                          _service.QueryStatus,
                                                                          new CommandID(GuidList.guidFormatPendingChangesCmdSet, (int)PkgCmdIDList.idUndoUnmodifiedChangesCommand));
                    mcs.AddCommand(undoUnmodifiedChangesCommand);
                }
            }
        }

        #endregion        
    }
}
