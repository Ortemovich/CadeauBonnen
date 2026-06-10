using ScradaCadeauBonnen_WPF.Bonnen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ScradaCadeauBonnen_WPF.Bonnen
{
    public class BonThemaSelector : DataTemplateSelector
    {
        public DataTemplate FancyTemplate { get; set; }
        public DataTemplate ModerneTemplate { get; set; }
        public DataTemplate FeestelijkeTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is BonViewModel bon)
            {
                switch (bon.ThemaId)
                {
                    case 1:
                        return FancyTemplate;
                    case 2:
                        return ModerneTemplate;
                    case 3:
                        return FeestelijkeTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
