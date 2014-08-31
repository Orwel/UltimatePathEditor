using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using UltimatePathEditor.ViewContract;

namespace UltimatePathEditor.Behavior
{
    /// <summary>
    /// Drop Behavior of ListBox
    /// </summary>
    class ListBoxDropBehavior : Behavior<ItemsControl>
    {
        #region Fields
        private PathValueDecorator _decorator = new PathValueDecorator();
        #endregion Fields

        #region Properties
        /// <summary>
        /// The dragged Path Value
        /// </summary>
        public IPathValueViewContract DragPathValue { get; set; }
        /// <summary>
        /// The type where can be dropped
        /// </summary>
        public Type DropType { get; set; }
        /// <summary>
        /// The type of the dragged Path Value
        /// </summary>
        private Type DataType { get { return DragPathValue == null ? null : DragPathValue.GetType(); } } //The type of data
        #endregion Properties

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.DragEnter += new DragEventHandler(AssociatedObject_DragEnter);
            this.AssociatedObject.DragOver += new DragEventHandler(AssociatedObject_DragOver);
            this.AssociatedObject.DragLeave += new DragEventHandler(AssociatedObject_DragLeave);
            this.AssociatedObject.Drop += new DragEventHandler(AssociatedObject_Drop);
        }

        void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            _decorator.Clear();
            if (this.DataType != null)
            {
                if (e.Data.GetDataPresent(DataType))
                {
                    HitTestResult hitTestResults = VisualTreeHelper.HitTest(this.AssociatedObject, e.GetPosition(this.AssociatedObject));
                    if (hitTestResults.VisualHit.GetType() == DropType)
                    {
                        var pathValue = this.GetPathValue(hitTestResults);
                        if(pathValue == null || pathValue == this.DragPathValue)
                            return;
                        _decorator.Decorate(pathValue);
                        e.Effects = DragDropEffects.Move;
                    }
                }
            }
            e.Handled = true;
        }

        void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            _decorator.Clear();
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            //if the data type can be dropped 
            if (this.DataType != null)
            {
                if (e.Data.GetDataPresent(DataType))
                {
                    HitTestResult hitTestResult = VisualTreeHelper.HitTest(this.AssociatedObject, e.GetPosition(this.AssociatedObject));
                    if (hitTestResult.VisualHit.GetType() == DropType)
                    {
                        var rootElement = sender as FrameworkElement;
                        if (rootElement == null)
                            return;
                        var pathVariable = rootElement.DataContext as IPathVariableViewContract;
                        if (pathVariable == null)
                            return;
                        var target = (hitTestResult.VisualHit as FrameworkElement).DataContext as IPathValueViewContract;
                        if (target == null)
                            return;
                        pathVariable.Drop(DragPathValue, target);
                    }
                }
            }
            _decorator.Clear();
            e.Handled = true;
        }

        /// <summary>
        /// Return a Path Value from hit result
        /// </summary>
        /// <param name="hitTestResult">the hit result</param>
        /// <returns>The Path Value from hit result or null if can not be found</returns>
        IPathValueViewContract GetPathValue(HitTestResult hitTestResult)
        {
            var element = hitTestResult.VisualHit as FrameworkElement;
            return element.DataContext as IPathValueViewContract;
        }
    }
}
