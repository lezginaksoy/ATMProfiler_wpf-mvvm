using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Common
{
    public class ModelAtmBrand
    {
        private string _BrandId;
        private string _BrandName;

        public ModelAtmBrand(string id,string name)
        {
            this._BrandId = id;
            this._BrandName = name;
        }

        public ModelAtmBrand()
        {
          
        }


        public string BrandId 
        {
            get
            {
                return _BrandId;
            }

            set
            {
                _BrandId = value;
            }
        }


        public string BrandName
        {
            get
            {
                return _BrandName;
            }

            set
            {
                _BrandName = value;
            }
        }



    }
}
