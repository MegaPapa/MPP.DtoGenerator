using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace MPP_DTOGenerator {
    
    [DataContract]
    public class DTOInstance {

        [DataMember]
        private List<DTOContainer> items = new List<DTOContainer>();

        public List<DTOContainer> Items {
            get { return items; }
            set { items = value; }
        }

    }
}
