﻿#pragma checksum "..\..\..\SideWindowCollection\EditDate.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CD56714BBD7D02FA6E7A8D0C3C6FEDFF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Task_Software.SideWindowCollection;


namespace Task_Software.SideWindowCollection {
    
    
    /// <summary>
    /// EditDate
    /// </summary>
    public partial class EditDate : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\SideWindowCollection\EditDate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid draggrid;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\SideWindowCollection\EditDate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\SideWindowCollection\EditDate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button savebutton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\SideWindowCollection\EditDate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button canclebutton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\SideWindowCollection\EditDate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datepicar;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\SideWindowCollection\EditDate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border plzwaitview;
        
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
            System.Uri resourceLocater = new System.Uri("/Task Manager;component/sidewindowcollection/editdate.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SideWindowCollection\EditDate.xaml"
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
            
            #line 8 "..\..\..\SideWindowCollection\EditDate.xaml"
            ((Task_Software.SideWindowCollection.EditDate)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.draggrid = ((System.Windows.Controls.Grid)(target));
            
            #line 17 "..\..\..\SideWindowCollection\EditDate.xaml"
            this.draggrid.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.draggrid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.savebutton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\SideWindowCollection\EditDate.xaml"
            this.savebutton.Click += new System.Windows.RoutedEventHandler(this.savebutton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.canclebutton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\SideWindowCollection\EditDate.xaml"
            this.canclebutton.Click += new System.Windows.RoutedEventHandler(this.canclebutton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.datepicar = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.plzwaitview = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

