using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                this.SetProperty(ref _value, value);
            }
        }

        public bool IsValid
        {
            get { return System.IO.Directory.Exists(Value); }
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
