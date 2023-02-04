using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSelectedObjects
{
    class cPlugin
    {
        public void Main(ref ETABSv1.cSapModel SapModel, ref ETABSv1.cPluginCallback ISapPlugin)
        {
            Form1 form = new Form1(ref SapModel, ref ISapPlugin);
            form.Show();
        }

        public long Info(ref string Text)
        {
            Text = "Beginnings of Diaphragm Slicer";
            return 0;
        }
    }
}
