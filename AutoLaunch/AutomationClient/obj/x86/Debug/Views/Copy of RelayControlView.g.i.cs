﻿#pragma checksum "..\..\..\..\Views\Copy of RelayControlView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B3DDCB4373FD9DAC8ACDE655741F37EF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
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
    /// RelayControlView
    /// </summary>
    public partial class RelayControlView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Views\Copy of RelayControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveBtn;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\Copy of RelayControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox portCmb;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Views\Copy of RelayControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox vendorCmb;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Views\Copy of RelayControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox relayNumberCmb;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Views\Copy of RelayControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox operationCmb;
        
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
            System.Uri resourceLocater = new System.Uri("/AutomationClient;component/views/copy%20of%20relaycontrolview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Copy of RelayControlView.xaml"
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
            
            #line 12 "..\..\..\..\Views\Copy of RelayControlView.xaml"
            this.saveBtn.Click += new System.Windows.RoutedEventHandler(this.saveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.portCmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.vendorCmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.relayNumberCmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.operationCmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

