using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace MPP_DTOGenerator {

    class DTOGenerator {

        private volatile int index;
        private volatile Object threadLock;
        private int maxThreadCount;
        private DTOContainer dtoContainer;
        private DTO dtoObject;
        private Semaphore semaphore;
        private String path;

        public DTOGenerator(String path,int maxThreadCount) {
            threadLock = new Object();
            this.maxThreadCount = maxThreadCount;
            index = 0;
            semaphore = new Semaphore(maxThreadCount, maxThreadCount);
            this.path = @path;
        }



        //parse Json file and return object from them
        public DTO ParseJSON(String path) {
            try {
                DTO result = null;
                FileStream stream = new FileStream(@path, FileMode.Open);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DTO));
                result = (DTO)serializer.ReadObject(stream);
                return result;
            }
            catch (IOException e) { 
                Console.WriteLine(e);
            }
            catch (SerializationException e) { 
                Console.WriteLine("Serializataion exception: {0}", e); 
            }
            return null;
        }

        //T4 code generation
        private String GenerateClassCode() {
            ClassTemplate templateInstance = new ClassTemplate();        // create template T4 object
            templateInstance.Session = new Dictionary<String, Object>();
            templateInstance.Session.Add("dtoContainer", dtoContainer);   // pass dtoContainer to ClassTemplate.tt
            templateInstance.Initialize();
            return templateInstance.TransformText();
        }


        //create .cs file
        private void CreateCSFile(object state) {
            lock (threadLock) {
                dtoContainer = ThreadSafeExecute();
                var outputFileName = path + dtoContainer.ClassName + ".cs";
                var generatedCode = GenerateClassCode();
                File.WriteAllText(outputFileName,generatedCode);
            }

            semaphore.Release();
        }

        //thread-safe execute dtocontainer from list
        private DTOContainer ThreadSafeExecute() {
            
            lock (threadLock) {
                DTOContainer tmpContainer = new DTOContainer();
                tmpContainer = dtoObject.Items.ElementAt(index);
                index++;
                return tmpContainer;
            }
            
        }

        //start generating .cs file (calling from main method)
        public void StartGenerating(DTO dtoObj) {
            dtoObject = dtoObj;
            for (int i = 0; i < dtoObject.Items.Count; i++) {
                semaphore.WaitOne();
                ThreadPool.QueueUserWorkItem(CreateCSFile);
            }
            
        }


    }
}
