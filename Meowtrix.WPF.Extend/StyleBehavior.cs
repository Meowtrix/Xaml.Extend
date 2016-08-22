using System;
using System.Windows;
using Meowtrix.Linq;

namespace Meowtrix.WPF.Extend
{
    public static class StyleBehavior
    {
        public static Style Merge(params Style[] styles)
        {
            var newstyle = new Style();
            foreach (var style in styles)
            {
                if (newstyle.TargetType == null) newstyle.TargetType = style.TargetType;
                else if (newstyle.TargetType != style.TargetType) throw new ArgumentException("Styles must target to same type");
                newstyle.Triggers.AddRange(style.Triggers);
                newstyle.Setters.AddRange(style.Setters);
                newstyle.Resources.MergedDictionaries.Add(style.Resources);
            }
            newstyle.Seal();
            return newstyle;
        }
    }
}
