using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ConsoleApplication1 {
    
    [DataContract]
    class Property {
        [DataMember]
        private String name;
        public String Name { 
            get { return name; } 
            set { name = value; } 
        }

        [DataMember]
        private String type;
        public String Type {
            get { return type; }
            set { type = value; }
        }

        [DataMember]
        private String format;
        public String Format {
            get { return format; }
            set { format = value; }
        }
    }

    [DataContract]
    class DTOContainer {

        [DataMember]
        private String className;
        public String ClassName {
            get { return className; }
            set { className = value; }
        }

        [DataMember]
        private List<Property> propertyList = new List<Property>();
        public List<Property> PropertyList {
            get { return propertyList; }
            set { propertyList = value; }
        }
    }
}
