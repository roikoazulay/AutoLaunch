﻿#pragma checksum "..\..\..\..\..\Views\Simulators\SshView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "72F02AE610B09D78EF04EB3EC7C75BCD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AutomationClient;
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


namespace AutomationClient.Views {
    
    
    /// <summary>
    /// WayneSim
    /// </summary>
    public partial class WayneSim : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\..\Views\Simulators\SshView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveBtn;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\Views\Simulators\SshView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox varCmb;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\Views\Simulators\SshView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userNameTxb;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\Views\Simulators\SshView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox passwordTxb;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\..\Views\Simulators\SshView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox commandCmb;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\Views\Simulators\SshView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox operationCmb;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\..\Views\Simulators\SshView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox logCmb;
        
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
            System.Uri resourceLocater = new System.Uri("/AutomationClient;component/views/simulators/sshview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Simulators\SshView.xaml"
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
            this.saveBtn = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\..\Views\Simulators\SshView.xaml"
            this.saveBtn.Click += new System.Windows.RoutedEventHandler(this.saveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.varCmb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 45 "..\..\..\..\..\Views\Simulators\SshView.xaml"
            this.varCmb.DropDownClosed += new System.EventHandler(this.varCmb_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.userNameTxb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.passwordTxb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.commandCmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.operationCmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.logCmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

