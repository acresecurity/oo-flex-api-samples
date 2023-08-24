using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using EnumsNET;
using NStack;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Terminal.Gui;
using Terminal.Gui.TextValidateProviders;

namespace Flex.Cli.Terminal.Gui
{
    internal class BaseReactiveView : Toplevel
    {
        protected readonly CompositeDisposable Disposable = new();
        
        #region Overrides of Toplevel

        protected override void Dispose(bool disposing)
        {
            Disposable.Dispose();
            base.Dispose(disposing);
        }

        #endregion

        protected (View label, View input) CreateTextFieldView<TSender>(View parent, View previous, string caption, TSender model, Expression<Func<TSender, string>> property)
            where TSender : class
        {
            var label = new Label(caption)
            {
                X = 1,
                Y = previous == null ? 1 : Pos.Bottom(previous) + 1,
                Width = 18
            };

            parent.Add(label);

            var input = new TextField(property.Compile().Invoke(model) ?? string.Empty)
            {
                X = Pos.Right(label) + 1,
                Y = previous == null ? 1 : Pos.Bottom(previous) + 1,
                Width = 50
            };

            model
                .WhenAnyValue(property)
                .BindTo(input, p => p.Text)
                .DisposeWith(Disposable);

            input
                .Events()
                .TextChanged
                .Select(p => input.Text)
                .DistinctUntilChanged()
                .BindTo(model, property)
                .DisposeWith(Disposable);

            parent.Add(input);

            return (label, input);
        }

        protected (View label, View field) CreateMaskedTextFieldView<TSender, TValue>(View parent, View previous, string caption, string mask, TSender model, Expression<Func<TSender, TValue>> property)
            where TSender : class
        {
            var label = new Label(caption)
            {
                X = 1,
                Y = previous == null ? 1 : Pos.Bottom(previous) + 1,
                Width = 18
            };

            parent.Add(label);

            var provider = new NetMaskedTextProvider(mask);
            var input = new TextMaskField(provider)
            {
                X = Pos.Right(label) + 1,
                Y = previous == null ? 1 : Pos.Bottom(previous) + 1,
                Width = 50,
                Text = property.Compile().Invoke(model).ToString()
            };

            model
                .WhenAnyValue(property)
                .Select(p => p.ToString())
                .BindTo(input, p => p.Text)
                .DisposeWith(Disposable);

            input
                .Events()
                .TextChanged
                .Select(p => input.Text)
                .DistinctUntilChanged()
                .Select(p =>
                {
                    var convert = TypeDescriptor.GetConverter(typeof(TValue));
                    return convert.ConvertFrom(p.ToString());
                })
                .BindTo(model, property)
                .DisposeWith(Disposable);

            parent.Add(input);

            return (label, input);
        }

        protected View CreateCheckBoxFieldView<TSender>(View parent, View previous, string caption, TSender model, Expression<Func<TSender, bool>> property)
            where TSender : class
        {
            var input = new CheckBox(caption)
            {
                X = 1,
                Y = previous == null ? 0 : Pos.Bottom(previous) + 1,
            };

            model
                .WhenAnyValue(property)
                .BindTo(input, p => p.Checked)
                .DisposeWith(Disposable);

            input
                .Events()
                .Toggled
                .Select(p => input.Checked)
                .DistinctUntilChanged()
                .BindTo(model, property)
                .DisposeWith(Disposable);

            parent.Add(input);

            return input;
        }

        protected (View label, View field) CreateRadioGroupFieldView<TSender, TEnum>(View parent, View previous, string caption, TSender model, Expression<Func<TSender, TEnum>> property)
            where TSender : class
            where TEnum : struct, Enum
        {
            var label = new Label(caption)
            {
                X = 1,
                Y = previous == null ? 1 : Pos.Bottom(previous) + 1,
                Width = 18
            };

            parent.Add(label);

            var enums = Enums.GetValues<TEnum>().ToArray();
            var value = property.Compile().Invoke(model);
            var index = Array.IndexOf(enums, value);

            var input = new RadioGroup(enums.Select(p => (ustring)p.ToString()).ToArray(), index)
            {
                X = Pos.Right(label) + 1,
                Y = previous == null ? 1 : Pos.Bottom(previous) + 1,
                Width = Dim.Fill(1),
                DisplayMode = DisplayModeLayout.Horizontal,
            };

            model
                .WhenAnyValue(property)
                .Select(p => Array.IndexOf(enums, p))
                .BindTo(input, p => p.SelectedItem)
                .DisposeWith(Disposable);

            input
                .Events()
                .SelectedItemChanged
                .Select(p => p.SelectedItem)
                .DistinctUntilChanged()
                .Select(p => enums[p])
                .BindTo(model, property)
                .DisposeWith(Disposable);

            parent.Add(input);

            return (label, input);
        }
    }
}
