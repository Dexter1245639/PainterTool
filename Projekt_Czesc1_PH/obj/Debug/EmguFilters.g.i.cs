﻿#pragma checksum "..\..\EmguFilters.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4D5FF258F8DE38A26247E26EA457F6B3FFACBB64E54C2823A56C00CAD4264A63"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using Projekt_Czesc1_PH;
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


namespace Projekt_Czesc1_PH {
    
    
    /// <summary>
    /// EmguFilters
    /// </summary>
    public partial class EmguFilters : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\EmguFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas imageSurface;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\EmguFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button loadImage;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\EmguFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button resetImage;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\EmguFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sobelFilter;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\EmguFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GuassianBlur;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\EmguFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CannyEdge;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\EmguFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BlackWhite;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\EmguFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ColorSharpness;
        
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
            System.Uri resourceLocater = new System.Uri("/Projekt_Czesc1_PH;component/emgufilters.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EmguFilters.xaml"
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
            this.imageSurface = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.loadImage = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\EmguFilters.xaml"
            this.loadImage.Click += new System.Windows.RoutedEventHandler(this.loadImage_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.resetImage = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\EmguFilters.xaml"
            this.resetImage.Click += new System.Windows.RoutedEventHandler(this.resetImage_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.sobelFilter = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\EmguFilters.xaml"
            this.sobelFilter.Click += new System.Windows.RoutedEventHandler(this.sobelFilter_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.GuassianBlur = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\EmguFilters.xaml"
            this.GuassianBlur.Click += new System.Windows.RoutedEventHandler(this.GuassianBlur_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CannyEdge = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\EmguFilters.xaml"
            this.CannyEdge.Click += new System.Windows.RoutedEventHandler(this.CannyEdge_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BlackWhite = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\EmguFilters.xaml"
            this.BlackWhite.Click += new System.Windows.RoutedEventHandler(this.BlackWhite_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ColorSharpness = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\EmguFilters.xaml"
            this.ColorSharpness.Click += new System.Windows.RoutedEventHandler(this.ColorSharpness_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

