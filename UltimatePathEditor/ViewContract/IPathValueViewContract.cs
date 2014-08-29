using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimatePathEditor.ViewContract
{
    /// <summary>
    /// Contract of view of Path Value
    /// </summary>
    interface IPathValueViewContract
    {
        /// <summary>
        /// Value of path
        /// </summary>
        string Value { get; set; }
        /// <summary>
        /// If Path is valid
        /// </summary>
        bool IsValid { get; }
        /// <summary>
        /// If Path Value is dragged
        /// </summary>
        bool IsDragged { get; set; }
        /// <summary>
        /// If Drag is over Path Value
        /// </summary>
        bool IsDragOver { get; set; }

    }
}
