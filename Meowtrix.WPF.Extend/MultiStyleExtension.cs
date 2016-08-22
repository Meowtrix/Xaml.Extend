using System;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace Meowtrix.WPF.Extend
{
    [MarkupExtensionReturnType(typeof(Style))]
    public class MultiStyleExtension : MarkupExtension
    {
        public string[] ResourceKeys { get; }
        public MultiStyleExtension(string resourceKeys)
        {
            if (resourceKeys == null) throw new ArgumentNullException(nameof(resourceKeys));
            ResourceKeys = resourceKeys.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (ResourceKeys.Length == 0)
                throw new ArgumentException("At least one style should be specified.");
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
            => StyleBehavior.Merge(ResourceKeys.Select(x =>
                {
                    object key;
                    if (x == ".")
                    {
                        IProvideValueTarget service = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
                        key = service.TargetObject.GetType();
                    }
                    else key = x;
                    return new StaticResourceExtension(key).ProvideValue(serviceProvider);
                }).Cast<Style>().ToArray());
    }
}
