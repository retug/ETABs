using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSelectedObjects
{
    public class DatabaseTableResult
    {
        public string [] FieldKeyList { get; set; }
        public int TableVersion { get; set; }
        public string [] FieldKeysIncluded { get; set; }
        public int NumberRecords { get; set; }
        public string [] TableData { get; set;  }

    }
}
