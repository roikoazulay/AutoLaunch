﻿#pragma checksum "..\..\..\SerachUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7ABF537D092C7DE575755B92B0F92267"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Controls;
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


namespace AutomationClient {
    
    
    /// <summary>
    /// SerachUserControl
    /// </summary>
    public partial class SerachUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\SerachUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTxb;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\SerachUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clearBtn;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\SerachUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button scriptSerachBtn;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\SerachUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button testSerachBtn;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\SerachUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button suiteSerachBtn;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\SerachUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid searchesDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/AutomationClient;component/serachusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SerachUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.searchTxb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.clearBtn = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\SerachUserControl.xaml"
            this.clearBtn.Click += new System.Windows.RoutedEventHandler(this.clearBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.scriptSerachBtn = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\SerachUserControl.xaml"
            this.scriptSerachBtn.Click += new System.Windows.RoutedEventHandler(this.scriptSerachBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.testSerachBtn = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\SerachUserControl.xaml"
            this.testSerachBtn.Click += new System.Windows.RoutedEventHandler(this.testSerachBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.suiteSerachBtn = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\SerachUserControl.xaml"
            this.suiteSerachBtn.Click += new System.Windows.RoutedEventHandler(this.suiteSerachBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.searchesDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 55 "..\..\..\SerachUserControl.xaml"
            this.searchesDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.searchesDataGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

