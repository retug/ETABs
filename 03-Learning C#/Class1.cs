﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETABSv1;

namespace ETABS_Plugin
{
    public class cPlugin
    {
        public void Main(ref ETABSv1.cSapModel SapModel, ref ETABSv1.cPluginCallback ISapPlugin)
        {
            Form1 form = new Form1(ref SapModel, ref ISapPlugin);
            form.Show();
        }

        public long Info(ref string Text)
        {
            Text = "Etabs plugin template created by Austin Guter";
            return 0;
        }
    }
}
