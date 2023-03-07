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
using LiveCharts;
using LiveCharts.Wpf;
using System.Drawing.Drawing2D;

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
        List<ETABS_Point> ETABsAreaPointsList;
        List<MyPoint> MyPoints;
        
        //the unique label of the starting point in ETABs
        string startPoint = null;
        VectorValue vectorXValue = new VectorValue();
        VectorValue vectorYValue = new VectorValue();

        



        public Form1(ref cSapModel SapModel, ref cPluginCallback Plugin)
        {
            _Plugin = Plugin;
            _SapModel = SapModel;
            InitializeComponent();
            

            
        }

        private void Form1_Paint(object send, PaintEventArgs e)
        {
            Graphics mgraphics = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(255, 140, 105), 1);
            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            System.Drawing.Drawing2D.LinearGradientBrush lGB2 = new System.Drawing.Drawing2D.LinearGradientBrush(area, Color.FromArgb(255, 255, 255), Color.FromArgb(159, 159, 159), LinearGradientMode.Vertical);
            mgraphics.FillRectangle(lGB2, area);
            mgraphics.DrawRectangle(pen, area);
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
                    startPoint = ObjectName[i];

                    SelectedObjectsList.Add(SelectedObject);
                }
                dataGridView1.DataSource = SelectedObjectsList;
            }
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

        }

        private void vectorX_TextChanged(object sender, EventArgs e)
        {
            VectorValue vectorXValue = new VectorValue();
            try
            {
                //need to fix this, currently this does not work
                vectorXValue.Value = double.Parse(vectorX.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("You must enter decimal number");
            }

        }
        private void vectorY_TextChanged(object sender, EventArgs e)
        {
            VectorValue vectorYValue = new VectorValue();
            try
            {
                //need to fix this, currently this does not work
                vectorYValue.Value = double.Parse(vectorY.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("You must enter decimal number");
            }

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

        public static double[] linspace(double startval, double endval, int steps)
        {
            double interval = (endval / Math.Abs(endval)) * Math.Abs(endval - startval) / (steps - 1);
            return (from val in Enumerable.Range(0, steps)
                    select startval + (val * interval)).ToArray();
        }

        private void makeSectionCutBtn(object sender, EventArgs e)
        {
            string TableKey = "Section Cut Definitions";
            //string TableKey = "Material Properties - General";

            //retreiving the table data review

            string[] FieldKeyList = new string[0];
            string GroupName = "";
            int TableVersion = 0;
            string[] FieldKeysIncluded = new string[0];
            int NumberRecords = 0;
            string[] TableData = new string[0];

            _SapModel.DatabaseTables.GetTableForDisplayArray(TableKey, ref FieldKeyList, GroupName, ref TableVersion, ref FieldKeysIncluded, ref NumberRecords, ref TableData);

            DatabaseTableResult resultsDatabaseTables = new DatabaseTableResult();
            resultsDatabaseTables.FieldKeyList = FieldKeyList;
            resultsDatabaseTables.TableVersion = TableVersion;
            resultsDatabaseTables.FieldKeysIncluded = FieldKeysIncluded;
            resultsDatabaseTables.NumberRecords = NumberRecords;
            resultsDatabaseTables.TableData = TableData;

            //trying to createa custom section cut, not working

            string[] testETABs_Section_Cut_Data = new string[] { };
            string[] teststring1 = new string[]
                { "0001", "Quads", "All", "Analysis", "Default", "0", "0", "0", "Top or Right or Positive3","1", "1", "1",
                    "0", "10", "0", "1"
                };
            string[] teststring2 = new string[]
                { "0001", null , null, null, null, null, null, null, null, null, "1", "2",
                    "10", "10", "0", null
                };
            string[] teststring3 = new string[]
                { "0001", null, null, null, null, null, null, null, null, null, "1", "3",
                    "10", "10", "1", null
                };
            string[] teststring4 = new string[]
                { "0001", null, null, null, null, null, null, null, null, null, "1", "4",
                    "0", "10", "1", null
                };
            testETABs_Section_Cut_Data = teststring1.Concat(teststring2).ToArray();
            testETABs_Section_Cut_Data = testETABs_Section_Cut_Data.Concat(teststring3).ToArray();
            testETABs_Section_Cut_Data = testETABs_Section_Cut_Data.Concat(teststring4).ToArray();



            int TableVersiontest = 1;
            string[] FieldKeysIncludedtest = new string[] {"Name", "DefinedBy", "Group", "ResultType", "ResultLoc", "RotAboutZ", "RotAboutY", "RotAboutX",
                "ElementSide", "NumQuads", "QuadNum", "PointNum", "QuadX", "QuadY", "QuadZ", "GUID"};
            int NumberRecordstest = 4;


            _SapModel.DatabaseTables.SetTableForEditingArray(TableKey, ref TableVersiontest, ref FieldKeysIncludedtest, NumberRecordstest, ref testETABs_Section_Cut_Data);

            bool FillImportLog = true;
            int NumFatalErrors = 0;
            int NumErrorMsgs = 0;
            int NumWarnMsgs = 0;
            int NumInfoMsgs = 0;
            string ImportLog = "";


            _SapModel.DatabaseTables.ApplyEditedTables(FillImportLog, ref NumFatalErrors, ref NumErrorMsgs, ref NumWarnMsgs, ref NumInfoMsgs, ref ImportLog);

            DatabaseTableInfo databaseTableInfo = new DatabaseTableInfo();
            databaseTableInfo.NumErrorMsgs = NumErrorMsgs;

            databaseTableInfo.ImportLog = ImportLog;

        }

        private void runAnalysis_Click(object sender, EventArgs e)
        {

            scatterPlot.Visible = true;
            momentScatterPlot.Visible = true;
            locationPlot.Visible = true;

            string Name = null;
            double X = 0;
            double Y = 0;
            double Z = 0;
            _SapModel.PointObj.GetCoordCartesian(startPoint, ref X, ref Y, ref Z);

            ETABS_Point ref_Point = new ETABS_Point();
            ref_Point.X = X;
            ref_Point.Y = Y;
            ref_Point.Z = Z;


            List<double> refPoint = new List<double>() { X, Y, Z };

            List<double> vector = new List<double>() { double.Parse(vectorX.Text), double.Parse(vectorY.Text), 0 };

            GlobalCoordinateSystem gcs = new GlobalCoordinateSystem(refPoint, vector);
            ETABsAreaPointsList = new List<ETABS_Point>();

            //this gathers all of the XYZ points of the area objects to determine the max and the minimum U and V values
            for (int i = 0; i < AreaPointList.Count; i++)
            {
                _SapModel.PointObj.GetCoordCartesian(AreaPointList[i].Points, ref X, ref Y, ref Z);
                ETABS_Point samplePoint = new ETABS_Point();
                samplePoint.X = X;
                samplePoint.Y = Y;
                samplePoint.Z = Z;
                //ETABsAreaPointsList.Add(samplePoint);
                List<double> myPoint = new List<double>() { X, Y, Z };

                MyPoint point = new MyPoint(myPoint);
                point.X = myPoint[0];
                point.Y = myPoint[1];
                point.Z = myPoint[2];


                //MyPoint point = new MyPoint(myPoint);
                point.glo_to_loc(gcs);
                ETABS_Point localPoint = new ETABS_Point();
                localPoint.X = point.LocalCoords[0];
                localPoint.Y = point.LocalCoords[1];
                localPoint.Z = point.LocalCoords[2];
                ETABsAreaPointsList.Add(localPoint);
                //MyPoints.Add(point);

            }
            

            //Finds the max U and V values
            double Umax = ETABsAreaPointsList.Max(x => x.X);
            double Umin = ETABsAreaPointsList.Min(x => x.X);
            double Vmax = ETABsAreaPointsList.Max(x => x.Y);
            double Vmin = ETABsAreaPointsList.Min(x => x.Y);



            double distance = Umax - Umin;

            int n_cuts = int.Parse(NumSlices.Text);
            double height = ref_Point.Z;

            // creates a list of values between max and min u values.
            double[] range_values = linspace(Umin + 1, Umax - 1, n_cuts);

            //creates the lists of 4 points
            List<List<MyPoint>> sectionPlanes = new List<List<MyPoint>>();
            List<List<String>> ETABs_Section_Cut_Data = new List<List<String>>();

            /////// Generating all of the data to be utilized to make the section cuts /////

            int counter = 0;
            foreach (double i in range_values)
            {
                List<MyPoint> sectionPlane = new List<MyPoint>();

                List<double> listPoint1 = new List<double>() { i, Vmin, height - 0.5 };
                List<double> listPoint2 = new List<double>() { i, Vmin, height + 0.5 };
                List<double> listPoint3 = new List<double>() { i, Vmax, height + 0.5 };
                List<double> listPoint4 = new List<double>() { i, Vmax, height - 0.5 };

                MyPoint sectionPoint1 = new MyPoint(listPoint1);
                MyPoint sectionPoint2 = new MyPoint(listPoint2);
                MyPoint sectionPoint3 = new MyPoint(listPoint3);
                MyPoint sectionPoint4 = new MyPoint(listPoint4);

                sectionPoint1.loc_to_glo(gcs);
                sectionPoint2.loc_to_glo(gcs);
                sectionPoint3.loc_to_glo(gcs);
                sectionPoint4.loc_to_glo(gcs);

                sectionPlane.Add(sectionPoint1);
                sectionPlane.Add(sectionPoint2);
                sectionPlane.Add(sectionPoint3);
                sectionPlane.Add(sectionPoint4);

                sectionPlanes.Add(sectionPlane);
                string name = counter.ToString().PadLeft(4, '0');

                List<string> string1 = new List<string>
                { name, "Quads", "All", "Analysis", "Default", "0", "0", "0", "Top or Right or Positive3", "1", "1", "1",
                    sectionPoint1.GlobalCoords[0].ToString(), sectionPoint1.GlobalCoords[1].ToString(), sectionPoint1.GlobalCoords[2].ToString(), "1"
                };
                List<string> string2 = new List<string>
                { name, null, null, null, null, null, null, null, null, null, "1", "2",
                    sectionPoint2.GlobalCoords[0].ToString(), sectionPoint2.GlobalCoords[1].ToString(), sectionPoint2.GlobalCoords[2].ToString(), null
                };
                List<string> string3 = new List<string>
                { name, null, null, null, null, null, null, null, null, null, "1", "3",
                    sectionPoint3.GlobalCoords[0].ToString(), sectionPoint3.GlobalCoords[1].ToString(), sectionPoint3.GlobalCoords[2].ToString(), null
                };
                List<string> string4 = new List<string>
                { name, null, null, null, null, null, null, null, null, null, "1", "4",
                    sectionPoint4.GlobalCoords[0].ToString(), sectionPoint4.GlobalCoords[1].ToString(), sectionPoint4.GlobalCoords[2].ToString(), null
                };

                ETABs_Section_Cut_Data.Add(string1);
                ETABs_Section_Cut_Data.Add(string2);
                ETABs_Section_Cut_Data.Add(string3);
                ETABs_Section_Cut_Data.Add(string4);
                counter++;
            }

            /////// Generating all of the data to be utilized to make the section cuts /////


            string TableKey = "Section Cut Definitions";
            string[] FieldKeysIncluded = ETABs_Section_Cut_Data.SelectMany(x => x).ToArray();

            int TableVersiontest = 1;
            string[] FieldKeysIncludedtest = new string[] { "Name", "DefinedBy", "Group", "ResultType", "ResultLoc", "RotAboutZ", "RotAboutY", "RotAboutX",
                "ElementSide", "NumQuads", "QuadNum", "PointNum", "QuadX", "QuadY", "QuadZ", "GUID" };
            int NumberRecordstest = ETABs_Section_Cut_Data.Count * 4;


            _SapModel.DatabaseTables.SetTableForEditingArray(TableKey, ref TableVersiontest, ref FieldKeysIncludedtest, NumberRecordstest, ref FieldKeysIncluded);

            bool FillImportLog = true;
            int NumFatalErrors = 0;
            int NumErrorMsgs = 0;
            int NumWarnMsgs = 0;
            int NumInfoMsgs = 0;
            string ImportLog = "";


            _SapModel.DatabaseTables.ApplyEditedTables(FillImportLog, ref NumFatalErrors, ref NumErrorMsgs, ref NumWarnMsgs, ref NumInfoMsgs, ref ImportLog);

            DatabaseTableInfo databaseTableInfo = new DatabaseTableInfo();
            databaseTableInfo.NumErrorMsgs = NumErrorMsgs;

            databaseTableInfo.ImportLog = ImportLog;

            _SapModel.Analyze.RunAnalysis();
            //sets to kip, ft, farienheit
            _SapModel.SetPresentUnits(eUnits.kip_ft_F);

            _SapModel.Results.Setup.SetCaseSelectedForOutput(LoadCaseComBox.SelectedItem.ToString());


            int NumberResults = 1;
            string[] SCut = new string[0];
            string[] LoadCase = new string[0];
            string[] StepType = new string[0];
            double[] StepNum = new double[0];
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _SapModel.Results.SectionCutAnalysis(ref NumberResults, ref SCut, ref LoadCase, ref StepType, ref StepNum, ref F1, ref F2, ref F3, ref M1, ref M2, ref M3);

            SectionResults sectionResults = new SectionResults();
            sectionResults.F1 = F1;
            sectionResults.F2 = F2;
            sectionResults.F3 = F3;
            sectionResults.M1 = M1;
            sectionResults.M2 = M2;
            sectionResults.M3 = M3;

            

            List<TabularData> TabDataList = new List<TabularData>();
            for (int i = 0; i < sectionResults.F2.Length; i++)
            {
                TabularData sampleData = new TabularData();
                sampleData.Location = range_values[i] / 12;
                sampleData.Shear = sectionResults.F2[i];
                sampleData.Moment = sectionResults.M3[i];

                TabDataList.Add(sampleData);
            }

            ////// Shear ScatterPlot //////////
            ///
            ChartValues<LiveCharts.Defaults.ObservablePoint> shearPoints = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>();

            for (int i = 0; i < sectionResults.F2.Length; i++)
            {
                shearPoints.Add(new LiveCharts.Defaults.ObservablePoint { X = range_values[i] / 12, Y = sectionResults.F2[i] });

            }

            var scatterShearSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "ShearSeries",
                Values = shearPoints,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 140, 105)),
                Fill = System.Windows.Media.Brushes.Transparent,


            };

            


            scatterPlot.AxisX.Clear();
            scatterPlot.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Location (ft)",
                Separator = new LiveCharts.Wpf.Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(2),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }

            }); ;
            scatterPlot.AxisY.Clear();
            scatterPlot.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Shear (kip)",
                Separator = new LiveCharts.Wpf.Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(2),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }


            });

            scatterPlot.Series.Add(scatterShearSeries);

            //scatterPlot.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(245, 245, 245));
            scatterPlot.Zoom = LiveCharts.ZoomingOptions.Xy;

            ////// Moment ScatterPlot //////////


            ChartValues<LiveCharts.Defaults.ObservablePoint> momentPoints = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>();

            for (int i = 0; i < sectionResults.M3.Length; i++)
            {

                momentPoints.Add(new LiveCharts.Defaults.ObservablePoint { X = range_values[i] / 12, Y = sectionResults.M3[i] });
            }


            var scatterMomentSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Moment (kip*ft)",
                Values = momentPoints,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 140, 105)),
                Fill = System.Windows.Media.Brushes.Transparent,


            };


            


            momentScatterPlot.AxisX.Clear();
            momentScatterPlot.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Location (ft)",
                Separator = new LiveCharts.Wpf.Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(2),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }

            });
            momentScatterPlot.AxisY.Clear();
            momentScatterPlot.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Moment (kip*ft)",
                Separator = new LiveCharts.Wpf.Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(2),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }


            });

            momentScatterPlot.Series.Add(scatterMomentSeries);

            //momentScatterPlot.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(245, 245, 245));
            momentScatterPlot.Zoom = LiveCharts.ZoomingOptions.Xy;



            ////// Location Plot //////////

            ChartValues<LiveCharts.Defaults.ObservablePoint> areaPoints = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>();

            for (int i = 0; i < ETABsAreaPointsList.Count(); i++)
            {
                areaPoints.Add(new LiveCharts.Defaults.ObservablePoint { X = ETABsAreaPointsList[i].X/12, Y = ETABsAreaPointsList[i].Y/12 });
            }


            var locationSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Area Locations",
                Values = areaPoints,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 140, 105)),
                Fill = System.Windows.Media.Brushes.Transparent,
            };

            //locationPlot.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(245, 245, 245));
            locationPlot.Series.Add(locationSeries);

            ChartValues<LiveCharts.Defaults.ObservablePoint> cutPoints = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>();

            for (int i = 0; i < range_values.Count(); i++)
            {

                cutPoints.Add(new LiveCharts.Defaults.ObservablePoint { X = Double.Parse(ETABs_Section_Cut_Data[i*4][12])/12, Y = Double.Parse(ETABs_Section_Cut_Data[i*4][13])/12 });
                cutPoints.Add(new LiveCharts.Defaults.ObservablePoint { X = Double.Parse(ETABs_Section_Cut_Data[i*4+2][12]) / 12, Y = Double.Parse(ETABs_Section_Cut_Data[i*4+2][13
                    ]) / 12 });
            }


            var cutSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Cut Locations",
                Values = cutPoints,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                Fill = System.Windows.Media.Brushes.Transparent,
            };

            locationPlot.Series.Add(cutSeries);

            dataGridView3.DataSource = TabDataList;

        }
    }
}

