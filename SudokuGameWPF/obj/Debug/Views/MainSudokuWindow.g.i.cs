﻿#pragma checksum "..\..\..\Views\MainSudokuWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5D1612B31429361306635325B8C9ACE50C88357D71D01B0E87EA71AE928D30A2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SudokuGameWPF;
using SudokuGameWPF.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SudokuGameWPF.Views {
    
    
    /// <summary>
    /// MainSudokuWindow
    /// </summary>
    public partial class MainSudokuWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Views\MainSudokuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainMenu_grid;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\MainSudokuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnPlay;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\MainSudokuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnContinue;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\MainSudokuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSettings;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Views\MainSudokuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnExit;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Views\MainSudokuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuGameWPF.Views.SettingsUserControl settingsScreen;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SudokuGameWPF;component/views/mainsudokuwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MainSudokuWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\Views\MainSudokuWindow.xaml"
            ((SudokuGameWPF.Views.MainSudokuWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainMenu_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.BtnPlay = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Views\MainSudokuWindow.xaml"
            this.BtnPlay.Click += new System.Windows.RoutedEventHandler(this.BtnNew_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnContinue = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Views\MainSudokuWindow.xaml"
            this.BtnContinue.Click += new System.Windows.RoutedEventHandler(this.BtnContinue_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnSettings = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\Views\MainSudokuWindow.xaml"
            this.BtnSettings.Click += new System.Windows.RoutedEventHandler(this.BtnSettings_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnExit = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\Views\MainSudokuWindow.xaml"
            this.BtnExit.Click += new System.Windows.RoutedEventHandler(this.BtnExit_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.settingsScreen = ((SudokuGameWPF.Views.SettingsUserControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

