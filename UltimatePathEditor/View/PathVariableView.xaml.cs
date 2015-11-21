using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UltimatePathEditor.ViewContract;

namespace UltimatePathEditor.View
{
    /// <summary>
    /// Interaction logic for PathVariableView.xaml
    /// </summary>
    public partial class PathVariableView : UserControl
    {
        public PathVariableView()
        {
            InitializeComponent();
        }

        #region Drag
        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
        	if(e.LeftButton != MouseButtonState.Pressed)
                return;
            var element = sender as FrameworkElement;
            if(element == null)
                return;
            var pathVariable = this.DataContext as IPathVariableViewContract;
            if (pathVariable == null)
                return;
            var pathValue = element.DataContext as IPathValueViewContract;
            if(pathValue == null)
                return;
            if(pathVariable.Drag(pathValue))
            {
                pathValue.IsDragged = true;
                listBoxDropBehavior.DropType = element.GetType();
                listBoxDropBehavior.DragPathValue = pathValue;
                DragDrop.DoDragDrop(element, pathValue, DragDropEffects.Move);
                pathValue.IsDragged = false;
            }
        }
        #endregion Drag
		
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null)
                return;
            var pathValue = ((FrameworkElement)sender).DataContext as IPathValueViewContract;
            if (pathValue == null)
                return;
            var opendFolder = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog {SelectedPath = pathValue.Value};
            if(opendFolder.ShowDialog() == true)
            {
                pathValue.Value = opendFolder.SelectedPath;
            }
        }
    }
}
