using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ETABSv1;

namespace GetSelectedObjects
{
    public partial class Form1 : Form
    {
        private cPluginCallback _Plugin = null;
        private cSapModel _SapModel = null;

        //initiate lists
        List<SelectedObjects> SelectedObjectsList;
        public Form1(ref cSapModel SapModel, ref cPluginCallback Plugin)
        {
            _Plugin = Plugin;
            _SapModel = SapModel;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // do setup things here
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // must include a call to finish()
            _Plugin.Finish(0);
        }

        private void getSelNodeBtn_Click(object sender, EventArgs e)
        {
            int NumberItems = 0;
            int[] ObjectType = null;
            string[] ObjectName = null;
            _SapModel.SelectObj.GetSelected(ref NumberItems, ref ObjectType, ref ObjectName);
            SelectedObjectsList = new List<SelectedObjects>();
            for (int i = 0; i < ObjectType.Length; i++)
            {
                SelectedObjects SelectedObject = new SelectedObjects();
                SelectedObject.ObjectType = ObjectType[i];
                SelectedObject.ObjectName = ObjectName[i];

                SelectedObjectsList.Add(SelectedObject);
            }
            //writes data to data
            dataGridView1.DataSource = SelectedObjectsList;


        }
    }
}
