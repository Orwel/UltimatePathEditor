using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace UltimatePathEditor.Behavior
{
    /// <summary>
    /// Override the undo/redo behavior of TextBox.
    /// If TextBox.Undo make nothing, then the event is not preempted.
    /// </summary>
    public class TextBoxOverrideUndoRedoRoutingBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            var textBox = this.AssociatedObject as TextBox;
            if (textBox == null)
                throw new ArgumentException("TextBoxUndoRedoIfNoEffectRoutingBehavior is to TextBox only.");
            textBox.KeyDown += TextBox_KeyDown;
            textBox.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.Z, ModifierKeys.Control));
        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    if (textBox.CanUndo && textBox.Undo())
                        e.Handled = true;
                }
            }
        }
    }
}
