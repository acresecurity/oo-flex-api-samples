using NStack;
using Terminal.Gui.TextValidateProviders;

// ReSharper disable once CheckNamespace
namespace Terminal.Gui
{
    internal class TextMaskField : TextValidateField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextValidateField"/> class using <see cref="LayoutStyle.Computed"/> positioning.
        /// </summary>
        public TextMaskField(ITextValidateProvider provider)
            : base(provider)
        {
        }

        /// <summary>
        ///   Changed event, raised when the text has changed.
        /// </summary>
        /// <remarks>
        ///   This event is raised when the <see cref="Text"/> changes. 
        /// </remarks>
        /// <remarks>
        ///   The passed <see cref="EventArgs"/> is a <see cref="ustring"/> containing the old value. 
        /// </remarks>
        public event Action<ustring> TextChanged;

        /// <summary>
        /// Text
        /// </summary>
        public new ustring Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                TextChanged?.Invoke(value);
            }
        }
    }
}
