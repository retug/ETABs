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

namespace ETABS_Plugin
{
    public partial class Form1 : Form
    {
        private cPluginCallback _Plugin = null;
        private cSapModel _SapModel = null;
        
        //initiate lists
        List<LoadCombination> LoadCombinationList;
        List<JointReaction> JointReactionList;


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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ShowLoadCombinationsBtn_Click(object sender, EventArgs e)
        {
            int NumberNames = 1;
            string[] Myname = null;

            _SapModel.RespCombo.GetNameList(ref NumberNames, ref Myname);

            LoadCombinationList = new List<LoadCombination>();

            for (int i = 0; i < Myname.Length; i++)
            {
                LoadCombination LComb = new LoadCombination();
                LComb.NumberNames = NumberNames;
                LComb.MyName = Myname[i];
                LoadCombinationComBox.Items.Add(Myname[i]);

      

                LoadCombinationList.Add(LComb);
            }
            dataGridView1.DataSource = LoadCombinationList;
        }

        private void ShowReactionFrocesBtn_Click(object sender, EventArgs e)
        {
            string Name = "";
            eItemTypeElm ItemTypeElm;
            int NumberResults = 1;
            string[] Obj = null;
            string[] Elm = null;
            string[] LoadCase = null;
            string[] StepType = null;
            double[] StepNum = null;
            double[] F1 = null;
            double[] F2 = null;
            double[] F3 = null;
            double[] M1 = null;
            double[] M2 = null;
            double[] M3 = null;
            int x = -1;
            _SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            //Method below acts on a string
            _SapModel.Results.Setup.SetComboSelectedForOutput(LoadCombinationComBox.SelectedItem.ToString());

            //unsure why eItemTypeElm.Element does not need to be initiated
            //anything that is ref is returned, not input
            x = _SapModel.Results.JointReact("4", eItemTypeElm.Element, ref NumberResults, ref Obj, ref Elm, ref LoadCase, ref StepType, ref StepNum, ref F1, ref F2, ref F3, ref M1, ref M2, ref M3);

            JointReactionList = new List<JointReaction>();
            //this class is from the other class that we created in a seperate file
            JointReaction JReact = new JointReaction();
            
            JReact.Name = Name;
            JReact.LoadCase = LoadCase[0];
            JReact.F1 = F1[0];
            JReact.F2 = F2[0];
            JReact.F3 = F3[0];
            JReact.M1 = M1[0];
            JReact.M2 = M2[0];
            JReact.M3 = M3[0];

            JointReactionList.Add(JReact);
            dataGridView1.DataSource = JointReactionList;


        }
    }
}
