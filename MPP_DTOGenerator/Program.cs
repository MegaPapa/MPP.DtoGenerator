using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

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
                DTOGenerator generator = new DTOGenerator(args[0],maxThreadCount);
                DTO dtoObject;
                dtoObject = generator.ParseJSON(args[1]);
                generator.StartGenerating(dtoObject);
                Console.WriteLine("Generating is complete.");
                Console.ReadKey();
            }
            else {
                Console.WriteLine("Invalid parameters count.");
                Console.ReadKey();
            }

        }


    }
}
