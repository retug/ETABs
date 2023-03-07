using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSelectedObjects
{
    class SectionResults
    {
		public int NumberResults { get; set; }
		public string[] SCut { get; set; }
		public string[] LoadCase { get; set; }
		public string[] StepType { get; set; }
		public double[] StepNum { get; set; }
		public double[] F1 { get; set; }
		public double[] F2 { get; set; }
		public double[] F3 { get; set; }
		public double[] M1 { get; set; }
		public double[] M2 { get; set; }
		public double[] M3 { get; set; }

	}

	class TabularData
    {
		public double Location { get; set; }
		public double Shear { get; set; }
		public double Moment { get; set; }
	}


}
