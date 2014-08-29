using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimatePathEditor.ViewContract;

namespace UltimatePathEditor.ViewModel
{
    /// <summary>
    /// Model of Path Variable View
    /// </summary>
    class PathVariableViewModel : BindableBase, IPathVariableViewContract
    {
        #region Fields
        private ObservableCollection<IPathValueViewContract> _pathValues = new ObservableCollection<IPathValueViewContract>();
        #endregion Fields

        #region Properties
        public ObservableCollection<IPathValueViewContract> PathValues
        {
            get { return this._pathValues; }
        }
        #endregion Properties

        public PathVariableViewModel()
        {
            var pathValues = Environment.GetEnvironmentVariable("Path").Split(';');
            foreach(var pathValue in pathValues)
            {
                this._pathValues.Add(new PathValueViewModel { Value = pathValue });
            }
        }
    }
}
