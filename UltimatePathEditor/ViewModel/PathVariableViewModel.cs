using System;
using System.Collections.ObjectModel;
using System.Linq;
using UltimatePathEditor.ViewContract;
using UltimatePathEditor.Model;
using System.Collections.Specialized;
using System.Windows.Input;

namespace UltimatePathEditor.ViewModel
{
    /// <summary>
    /// Model of Path Variable View
    /// </summary>
    class PathVariableViewModel : BindableBase, IPathVariableViewContract
    {
        #region Fields
        private readonly ObservableCollection<IPathValueViewContract> _pathValues = new ObservableCollection<IPathValueViewContract>();
        private bool _modifyState = false;
        private readonly RelayCommand _purgeCommand;
        private readonly RelayCommand _undoCommand;
        private readonly RelayCommand _redoCommand;
        #endregion Fields

        #region Properties
        public ObservableCollection<IPathValueViewContract> PathValues
        {
            get { return this._pathValues; }
        }

        public ICommand PurgeCommand { get { return this._purgeCommand; } }

        public ICommand UndoCommand { get { return this._undoCommand; } }

        public ICommand RedoCommand { get { return this._redoCommand; } }
        #endregion Properties

        public PathVariableViewModel()
        {
            this._purgeCommand = new RelayCommand(o => this.PurgeUnvalidPathValue());
            this._undoCommand = new RelayCommand(o => this.Undo());
            this._redoCommand = new RelayCommand(o => this.Redo());
            this._pathValues.CollectionChanged += PathValues_CollectionChanged;
            Refresh();
        }

        /// <summary>
        /// Refresh the list of Path Value
        /// </summary>
        public void Refresh()
        {
            Refresh(PathVariableManager.Instance.GetEnvironmentVariable());
        }

        /// <summary>
        /// Refresh the list of Path Value
        /// </summary>
        public void Refresh(string pathEnvironmentVariable)
        {
            _modifyState = true;
            this._pathValues.Clear();
            var pathValues = pathEnvironmentVariable.Split(PathVariableManager.SplitCharacter);
            foreach (var pathValue in pathValues)
            {
                this._pathValues.Add(new PathValueViewModel { Value = pathValue });
            }
            _modifyState = false;
        }

        /// <summary>
        /// Send the Environment Varaible Path to DAL
        /// </summary>
        private void SendEnvironmentVariable()
        {
            var tmp = _pathValues.Aggregate(string.Empty, (current, pathValue) => current + (pathValue.Value + PathVariableManager.SplitCharacter));
            PathVariableManager.Instance.SetEnvironmentVariableMemento(tmp);
        }

        /// <summary>
        /// Remove Unvalid path value
        /// </summary>
        private void PurgeUnvalidPathValue()
        {
            this._modifyState = true;
            int i = 0;
            while(i<this._pathValues.Count)
            {
                if (this._pathValues[i].IsValid)
                    i++;
                else
                    this._pathValues.RemoveAt(i);
            }
            this._modifyState = false;
            this.SendEnvironmentVariable();
        }

        /// <summary>
        /// Undo a modification of the Environment Variable Path
        /// </summary>
        private void Undo()
        {
            var result = PathVariableManager.Instance.Undo();
            if (result != null)
                Refresh(result);
        }

        /// <summary>
        /// Redo a modification of the Environment Variable Path
        /// </summary>
        private void Redo()
        {
            var result = PathVariableManager.Instance.Redo();
            if (result != null)
                Refresh(result);
        }

        #region Subscribe
        void PathValues_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add :
                    foreach(PathValueViewModel pathValue in e.NewItems)
                    {
                        pathValue.PropertyChanged += PathValueViewModel_PropertyChanged;
                    }
                    break;
            }
        }

        void PathValueViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var PathValue = sender as PathValueViewModel;
            if(PathValue != null && e.PropertyName == "Value")
            {
                bool ownerModifyState = !_modifyState; //Responsabilty to modify Path Environment Variable at end of operation
                _modifyState = true;
                if (String.IsNullOrEmpty(PathValue.Value))
                {
                    _pathValues.Remove(PathValue);
                }
                else
                {
                    var arrayPathValue = PathValue.Value.Split(PathVariableManager.SplitCharacter);
                    if (arrayPathValue.Length > 1)
                    {
                        PathValue.Value = IfNullOrEmptyReturnNew(arrayPathValue.First());
                        int baginIndex = _pathValues.IndexOf(PathValue);
                        for (int i = 1; i < arrayPathValue.Length; i++)
                            _pathValues.Insert(baginIndex + i, new PathValueViewModel {
                                Value = IfNullOrEmptyReturnNew(arrayPathValue[i])
                            });
                    }
                }
                if (ownerModifyState)
                {
                    this.SendEnvironmentVariable();
                    _modifyState = false;
                }
            }
        }
        #endregion Subscribe

        private static string IfNullOrEmptyReturnNew(string value)
        {
            return String.IsNullOrEmpty(value) ? "New" : value;
        }

        #region Drag and Drop
        public bool Drag(IPathValueViewContract pathValue)
        {
            return this._pathValues.Contains(pathValue);
        }

        public void Drop(IPathValueViewContract pathValue, IPathValueViewContract target)
        {
            this._modifyState = true;
            if (pathValue != target)
                if (this._pathValues.Remove(pathValue))
                    this._pathValues.Insert(this._pathValues.IndexOf(target) + 1, pathValue);
            this.SendEnvironmentVariable();
            this._modifyState = false;
        }
        #endregion Drag and Drop
    }
}
