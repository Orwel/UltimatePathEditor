using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;

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

        /// <summary>
        /// Command to execute purge of the unvalid path values
        /// </summary>
        ICommand PurgeCommand { get; }

        /// <summary>
        /// Cancel the last modification of the Environment Variable Path
        /// </summary>
        ICommand UndoCommand { get; }

        /// <summary>
        /// Redo the last modification of the Environment Variable Path
        /// </summary>
        ICommand RedoCommand { get; }

        /// <summary>
        /// If return true, begin the drag interaction.
        /// </summary>
        /// <param name="pathValue">The Path Value will be dragged</param>
        /// <returns>True if Drag is accepted</returns>
        bool Drag(IPathValueViewContract pathValue);

        /// <summary>
        /// Drop/Move the Path Value in the list
        /// </summary>
        /// <param name="pathValue">The dropped Path Value</param>
        /// <param name="target">Where is dropped the Path Value</param>
        void Drop(IPathValueViewContract pathValue, IPathValueViewContract target);
    }
}
