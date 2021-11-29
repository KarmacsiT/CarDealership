using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Models
{
   public class ChangeNumericDataHelper
   {
        public int searchedID { get; set; }
        public string valueType { get; set; }
        public int newValue { get; set; }

        public ChangeNumericDataHelper(int searchedID, string valueType, int newValue)
        {
            this.searchedID = searchedID;
            this.valueType = valueType;
            this.newValue = newValue;
        }
   }
}
