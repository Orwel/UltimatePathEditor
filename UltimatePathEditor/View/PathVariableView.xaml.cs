using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        private void Ellipse_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
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
            pathVariable.Drag(pathValue);
            listBoxDropBehavior.DropType = element.GetType();
            listBoxDropBehavior.DragPathValue = pathValue;
            DragDrop.DoDragDrop(element, pathValue, DragDropEffects.Move);
        }
        #endregion Drag
		
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null)
                return;
            var pathValue = ((FrameworkElement)sender).DataContext as IPathValueViewContract;
            if (pathValue == null)
                return;
            var opendFolder = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            opendFolder.SelectedPath = pathValue.Value;
            if(opendFolder.ShowDialog() == true)
            {
                pathValue.Value = opendFolder.SelectedPath;
            }
        }
    }
}
