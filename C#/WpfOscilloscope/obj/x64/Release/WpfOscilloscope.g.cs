﻿#pragma checksum "..\..\..\WpfOscilloscope.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "44718DA86727A49F1278C82685036BF11D28AFB13C00511C3D83CF818D10CA73"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using SciChart.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common;
using SciChart.Charting.Common.AttachedProperties;
using SciChart.Charting.Common.Databinding;
using SciChart.Charting.Common.Extensions;
using SciChart.Charting.Common.Helpers;
using SciChart.Charting.Common.MarkupExtensions;
using SciChart.Charting.HistoryManagers;
using SciChart.Charting.Model;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Numerics.CoordinateProviders;
using SciChart.Charting.Numerics.DeltaCalculators;
using SciChart.Charting.Numerics.TickProviders;
using SciChart.Charting.Themes;
using SciChart.Charting.ViewportManagers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.Axes.DiscontinuousAxis;
using SciChart.Charting.Visuals.Axes.LabelProviders;
using SciChart.Charting.Visuals.Axes.LogarithmicAxis;
using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals.RenderableSeries.Animations;
using SciChart.Charting.Visuals.RenderableSeries.DrawingProviders;
using SciChart.Charting.Visuals.Shapes;
using SciChart.Charting.Visuals.TradeChart;
using SciChart.Charting.Visuals.TradeChart.MultiPane;
using SciChart.Core.AttachedProperties;
using SciChart.Core.MarkupExtensions;
using SciChart.Core.Utility.Mouse;
using SciChart.Data.Model;
using SciChart.Data.Numerics;
using SciChart.Drawing;
using SciChart.Drawing.Common;
using SciChart.Drawing.Extensions;
using SciChart.Drawing.HighQualityRasterizer;
using SciChart.Drawing.HighSpeedRasterizer;
using SciChart.Drawing.VisualXcceleratorRasterizer;
using SciChart.Drawing.XamlRasterizer;
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
using WpfOscilloscopeControl;


namespace WpfOscilloscopeControl {
    
    
    /// <summary>
    /// WpfOscilloscope
    /// </summary>
    public partial class WpfOscilloscope : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\WpfOscilloscope.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SciChart.Charting.Visuals.SciChartSurface sciChart;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\WpfOscilloscope.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SciChart.Charting.ChartModifiers.RubberBandXyZoomModifier RubberBandXyZoomModifier;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\WpfOscilloscope.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SciChart.Charting.ChartModifiers.ZoomPanModifier ZoomPanModifier;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\WpfOscilloscope.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SciChart.Charting.ChartModifiers.RolloverModifier RolloverModifier;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfOscilloscopeControl;component/wpfoscilloscope.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WpfOscilloscope.xaml"
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
            this.sciChart = ((SciChart.Charting.Visuals.SciChartSurface)(target));
            return;
            case 2:
            this.RubberBandXyZoomModifier = ((SciChart.Charting.ChartModifiers.RubberBandXyZoomModifier)(target));
            return;
            case 3:
            this.ZoomPanModifier = ((SciChart.Charting.ChartModifiers.ZoomPanModifier)(target));
            return;
            case 4:
            this.RolloverModifier = ((SciChart.Charting.ChartModifiers.RolloverModifier)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

