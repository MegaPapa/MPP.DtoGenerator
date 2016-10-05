using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace MPP_DTOGenerator {
    class Program {

        

        static void Main(string[] args) {
                if (args.Length == 2) {
                    if ((!Directory.Exists(@args[0])) || (!File.Exists(@args[1]))) {
                        Console.WriteLine("Invalid path.");
                        Console.ReadKey();
                        return;
                    }
                    var maxThreadCount = Int32.Parse(ConfigurationSettings.AppSettings["maxThreadCount"]);
                    DTOGenerator generator = new DTOGenerator(args[0], maxThreadCount);
                    DTOInstance dtoObject;
                    dtoObject = JSONParser.ParseJSON(args[1]);
                    generator.StartGenerating(dtoObject);
                    Console.WriteLine("Generating is complete. Press key.");
                    Console.ReadKey();
                }
                else {
                    //throw new Exception();
                    Console.WriteLine("Invalid parameters count.");
                    Console.ReadKey();
                }
        }


    }
}
