using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace UltimatePathEditor.Behavior
{
    /// <summary>
    /// Add auto scroll in Drag Drop interaction, 
    /// Thank Robert Kopferl
    /// </summary>
    class DragDropScrollBehavior : Behavior<ItemsControl>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.PreviewDragOver += OnContainerPreviewDragOver;
        }

        private DateTime s_lastTime = DateTime.MinValue;

        private void OnContainerPreviewDragOver(object sender, DragEventArgs e)
        {
            FrameworkElement container = sender as FrameworkElement;
            if (container == null)
                return;

            // determine Item-wise or content scrolling (by pixel)
            bool itemwise = (bool)container.GetValue(ScrollViewer.CanContentScrollProperty);


            // record time and execute only so often - store time singular static
            // this does not restrict to scroll at two places at a time (how would that go anyways)
            // but only syncs them in time.... that's fair enough; (300ms for ListBox, 20ms for Content)
            TimeSpan span = DateTime.UtcNow - s_lastTime;
            if (span.Milliseconds < (itemwise ? 300 : 20))
                return;
            s_lastTime = DateTime.UtcNow;

            // digg out the scrollviewer in question
            ScrollViewer scrollViewer = GetFirstVisualChild<ScrollViewer>(container);
            if (scrollViewer == null)
                return;

            //==============//////////// actual begin ================
            // base Tolerance on ActualHeight and make sensitive area relative but at max a constant size
            const double k_maxTolerance = 40;
            double actualHeight = scrollViewer.ActualHeight;
            // try max 25% of height (4 sml ctrl) and limit to max so the regions don't become too 
            // big but also the sensitive regions never overlap
            double tolerance = Math.Min(k_maxTolerance, actualHeight * 0.25);
            double verticalPos = e.GetPosition(scrollViewer).Y;
            // for list box go as fast as maximum 3 (leave some room to hit->0.35 more) for content jump 30;
            double offset = itemwise ? 3.35 : 30d;

            if (verticalPos < tolerance) // Top of visible list? 
            {
                // accelerate offset * 0..1
                offset = offset * ((tolerance - verticalPos) / tolerance);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - offset); //Scroll up. 
            }
            else if (verticalPos > actualHeight - tolerance) //Bottom of visible list? 
            {
                // accelerate offset * 0..1
                offset = offset * ((tolerance - (actualHeight - verticalPos)) / tolerance);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + offset); //Scroll down.     
            }

        }

        private void Unsubscribe(FrameworkElement container)
        {
            container.PreviewDragOver -= OnContainerPreviewDragOver;
        }

        public T GetFirstVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = GetFirstVisualChild<T>(child);
                    if (childItem != null)
                    {
                        return childItem;
                    }
                }
            }

            return null;
        }
    }
}
