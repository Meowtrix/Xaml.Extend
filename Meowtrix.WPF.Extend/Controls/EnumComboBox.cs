using System;
using System.Windows.Controls;

namespace Meowtrix.WPF.Extend.Controls
{
    public class EnumComboBox : ComboBox
    {
        private Type _enumtype;
        public Type EnumType
        {
            get { return _enumtype; }
            set
            {
                if (!value.IsEnum) throw new ArgumentException(nameof(EnumType));
                _enumtype = value;
                BuildItemsSource(value);
            }
        }

        private void BuildItemsSource(Type type)
        {

        }
    }
}
