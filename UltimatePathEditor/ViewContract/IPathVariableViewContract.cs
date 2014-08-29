using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace UltimatePathEditor.ViewContract
{
    /// <summary>
    /// Contract of Path Variable View
    /// </summary>
    [InheritedExport]
    interface IPathVariableViewContract
    {
        /// <summary>
        /// Collection of Value in Path Environment Variable
        /// </summary>
        ObservableCollection<IPathValueViewContract> PathValues { get; }
    }
}
