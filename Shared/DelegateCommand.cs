using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using System.Collections.Specialized;
#if SILVERLIGHT
using System.Linq;
#endif

namespace OpenOptions.dnaFusion.Flex.V1
{
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _CanExecute;

        private readonly Action<object> _Method;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                _ControlEvent.Add(new WeakReference(value));
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
#if SILVERLIGHT
                var query = (from q in ControlEvent where ((EventHandler)q.Target) == value select q).FirstOrDefault();
                if (query != null)
                    ControlEvent.Remove(query);
#else
                _ControlEvent.Remove(_ControlEvent.Find(r => ((EventHandler)r.Target) == value));
#endif
            }
        }

        private List<INotifyPropertyChanged> _PropertiesToListenTo;
        private readonly List<WeakReference> _ControlEvent;

        public DelegateCommand(Action<object> method)
            : this(method, null)
        {
        }

        public DelegateCommand(Action<object> method, Predicate<object> canExecute)
        {
            _ControlEvent = new List<WeakReference>();
            _Method = method;
            _CanExecute = canExecute;
        }

        public List<INotifyPropertyChanged> PropertiesToListenTo
        {
            get
            {
                return _PropertiesToListenTo;
            }
            set
            {
                _PropertiesToListenTo = value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_CanExecute == null)
                return true;
            return _CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _Method.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (_ControlEvent != null && _ControlEvent.Count > 0)
            {
                _ControlEvent.ForEach(ce =>
                {
                    if (ce.Target != null)
                        ((EventHandler)(ce.Target)).Invoke(null, EventArgs.Empty);
                });
            }
        }

        public DelegateCommand ListenOn<TObservedType, TPropertyType>(TObservedType viewModel, Expression<Func<TObservedType, TPropertyType>> propertyExpression) where TObservedType : INotifyPropertyChanged
        {
            string propertyName = GetPropertyName(propertyExpression);
            viewModel.PropertyChanged += (PropertyChangedEventHandler)((sender, e) =>
            {
                if (e.PropertyName == propertyName)
                    RaiseCanExecuteChanged();
            });
            return this;
        }

        public void ListenForNotificationFrom<TObservedType>(TObservedType viewModel) where TObservedType : INotifyPropertyChanged
        {
            viewModel.PropertyChanged += (PropertyChangedEventHandler)((sender, e) =>
            {
                RaiseCanExecuteChanged();
            });
        }

        public DelegateCommand ListenOn<TObservedType>(TObservedType collection) where TObservedType : INotifyCollectionChanged
        {
            collection.CollectionChanged += (NotifyCollectionChangedEventHandler)((sender, e) =>
            {
                RaiseCanExecuteChanged();
            });
            return this;
        }

        private string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression) where T : INotifyPropertyChanged
        {
            var lambda = expression as LambdaExpression;
            MemberInfo memberInfo = GetmemberExpression(lambda).Member;
            return memberInfo.Name;
        }

        private MemberExpression GetmemberExpression(LambdaExpression lambda)
        {
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
                memberExpression = lambda.Body as MemberExpression;
            return memberExpression;
        }
    }
}
