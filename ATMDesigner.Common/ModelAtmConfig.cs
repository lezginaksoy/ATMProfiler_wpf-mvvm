using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Common
{
    public class ModelAtmConfig
    {

        private string _Code;
        private string _Name;

        public ModelAtmConfig(string code,string name)
        {
            this._Code = code;
            this._Name = name;
        }

        public ModelAtmConfig()
        {
          
        }

        public string Code
        {
            get
            {
                return _Code;
            }

            set
            {
                _Code = value;
            }
        }


        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

    }
}
