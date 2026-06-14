using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Combobox
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public Guid Value { get; set; }

        public override string ToString()
        {
            return Text;        // This is important! It shows the Role Name in the ComboBox
        }
    }
}
