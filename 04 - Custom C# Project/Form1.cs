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
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Data.Text;

namespace GetSelectedObjects
{
    public partial class Form1 : Form
    {
        private cPluginCallback _Plugin = null;
        private cSapModel _SapModel = null;

        //initiate lists
        List<SelectedObjects> SelectedObjectsList;
        List<LoadCase> LoadCaseList;
        List<AreaPoint> AreaPointList;
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

        private void ShowLoadCase_Load(object sender, EventArgs e)
        {
            int NumberNames = 1;
            string[] MyName = null;

            _SapModel.LoadCases.GetNameList(ref NumberNames, ref MyName);

            LoadCaseList = new List<LoadCase>();

            for (int i = 0; i < MyName.Length; i++)
            {
                LoadCase LComb = new LoadCase();
                LComb.NumberNames = NumberNames;
                LComb.MyName = MyName[i];
                LoadCaseComBox.Items.Add(MyName[i]);



                LoadCaseList.Add(LComb);
            }
        }

        private void getSelNodeBtn_Click(object sender, EventArgs e)
        {
            int NumberItems = 0;
            int[] ObjectType = null;
            string[] ObjectName = null;
            _SapModel.SelectObj.GetSelected(ref NumberItems, ref ObjectType, ref ObjectName);
            SelectedObjectsList = new List<SelectedObjects>();
            //test to make sure the selected object is only 1 element long and a node
            if (ObjectType.Length > 1 || ObjectType[0] != 1)
            {
                MessageBox.Show("Select only one node");
            }
            else
            {
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
            //playing with matrices

            //double[,] x = {{ 1.0, 2.0 },
            //  { 3.0, 4.0 }};

            //Matrix<double> customMatrix = Matrix<double>.Build.DenseOfArray(x);
            //Matrix<double> inverseMyCustomMatrix = customMatrix.Inverse();

            //string MatrixTextInverste = inverseMyCustomMatrix.ToString("F2");


            //Matrix<double> myMatrix = Matrix<double>.Build.Random(3, 4);
            //string MatrixText = myMatrix.ToString("F2");
            //Matrix<double> inverseMyMatrix = myMatrix.Inverse();

        }

        private void getSelAreas_Click(object sender, EventArgs e)
        {
            int NumberItems = 0;
            int[] ObjectType = null;
            string[] ObjectName = null;
            _SapModel.SelectObj.GetSelected(ref NumberItems, ref ObjectType, ref ObjectName);
            SelectedObjectsList = new List<SelectedObjects>();
            AreaPointList = new List<AreaPoint>();



            for (int i = 0; i < ObjectType.Length; i++)
            {
                SelectedObjects SelectedObject = new SelectedObjects();
                SelectedObject.ObjectType = ObjectType[i];
                SelectedObject.ObjectName = ObjectName[i];

                int NumberAreaPoints = 0;
                string[] ObjectNamePnts = null;
                

                //if the object type is 5, this is a floor
                if (ObjectType[i] == 5)
                {
                    
                    SelectedObjectsList.Add(SelectedObject);
                    _SapModel.AreaObj.GetPoints(ObjectName[i], ref NumberAreaPoints, ref ObjectNamePnts);
                    
                    //gather all of the points in an individual area object
                    //AreaPointObject.NumberPoints = NumberAreaPoints;
                    for (int j = 0; j < ObjectNamePnts.Length; j++)
                    {
                        AreaPoint AreaPointObject = new AreaPoint();
                        AreaPointObject.NumberPoints = NumberAreaPoints;
                        AreaPointObject.Points = ObjectNamePnts[j];
                        AreaPointList.Add(AreaPointObject);
                    }
                }
                    
                else
                {
                    ;
                }
            }
            //writes data to data
            dataGridView2.DataSource = SelectedObjectsList;
            dataGridView3.DataSource = AreaPointList;
        }

        private void vectorX_TextChanged(object sender, EventArgs e)
        {

        }
        //function below limits the input to a decimal number
        private void vectorX_KeyPress(object sender, KeyPressEventArgs e)
        {
            vectorX.MaxLength = 6;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void vectorX_Enter(object sender, EventArgs e)
        {
            vectorX.Text = "";
            vectorX.ForeColor = Color.Black;
            
        }
        private void vectorY_Enter(object sender, EventArgs e)
        {
            vectorY.Text = "";
            vectorY.ForeColor = Color.Black;
        }


        //limits input to numbers
        private void numSlices_KeyPress(object sender, KeyPressEventArgs e)
        {
            vectorX.MaxLength = 6;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
        }

        //this function checks to make sure inputted value is above 2 and below 1000
        private void NumSlices_Leave(object sender, EventArgs e)
        {
            int num_slices = 0;
            Int32.TryParse(NumSlices.Text, out num_slices);
            if (num_slices < 1 && NumSlices.Text != "")
            {
                NumSlices.Text = "2";
                MessageBox.Show("Minimum Allowed Cuts is 2");

            }
            else if (num_slices > 1000 && NumSlices.Text != "")
            {
                NumSlices.Text = "1000";
                MessageBox.Show("Maximum Allowed Cuts is 1000");

            }
        }
    }
}
