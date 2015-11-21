using UltimatePathEditor.ViewContract;

namespace UltimatePathEditor.ViewModel
{
    /// <summary>
    /// Model of Path Value View
    /// </summary>
    class PathValueViewModel : BindableBase, IPathValueViewContract
    {
        #region Fields
        private string _value;
        private bool _isDragged;
        private bool _isDragOver;
        private bool _isValid;
        #endregion Fields

        #region Properties
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this.SetProperty(ref this._value, value);
                IsValid = System.IO.Directory.Exists(Value);
            }
        }

        public bool IsValid
        {
            get
            {
                return this._isValid;
            }
            private set
            {
                this.SetProperty(ref this._isValid, value);
            }
        }

        public bool IsDragged
        {
            get
            {
                return this._isDragged;
            }
            set
            {
                this.SetProperty(ref _isDragged, value);
            }
        }

        public bool IsDragOver
        {
            get
            {
                return this._isDragOver;
            }
            set
            {
                this.SetProperty(ref _isDragOver, value);
            }
        }
        #endregion Properties
    }
}
