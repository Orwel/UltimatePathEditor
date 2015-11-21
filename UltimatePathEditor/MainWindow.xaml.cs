using System.Windows;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using UltimatePathEditor.ViewContract;

namespace UltimatePathEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Import the contract from composition to DataContext
        /// </summary>
        [Import]
        private IPathVariableViewContract PathVariable { set { this.DataContext = value; } }

        public MainWindow()
        {
            InitializeComponent();

            var catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}
