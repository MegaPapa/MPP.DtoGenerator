using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace MPP_DTOGenerator {
    public class JSONParser {

        //parse Json file and return object from them
        public static DTOInstance ParseJSON(String path) {
                DTOInstance result = null;
                FileStream stream = new FileStream(@path, FileMode.Open);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DTOInstance));
                result = (DTOInstance)serializer.ReadObject(stream);
                return result;
            
        }

    }
}
