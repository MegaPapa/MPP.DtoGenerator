using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;


namespace ConsoleApplication1 {

    class Program {

        private static List<DTOContainer> items = new List<DTOContainer>();

        private static void addItem(String className, String name, String type, String format) {
            DTOContainer tmpContainer = new DTOContainer();
            tmpContainer.ClassName = className;
            Property tmpProperty = new Property();
            tmpProperty.Format = format;
            tmpProperty.Name = name;
            tmpProperty.Type = type;
            tmpContainer.PropertyList.Add(tmpProperty);
            items.Add(tmpContainer);
        }

        private static void serizalization(String path) {
            FileStream stream = File.Create(path);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<DTOContainer>));
            serializer.WriteObject(stream, items);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            Console.Write("JSON form of Person object: ");
            Console.WriteLine(sr.ReadToEnd());
        }


        static void Main(string[] args) {
            addItem("class1","name1","type1","format1");
            addItem("class2", "name1", "type1", "format1");
            addItem("class3", "name1", "type1", "format1");
            serizalization("C:\\Users\\Public\\Desktop\\test.json");
            Console.ReadKey();
        }
    }
}
