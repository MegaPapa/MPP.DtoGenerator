using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using MPP_DTOGenerator.DTOLib;

namespace MPP_DTOGenerator {

    public class DTOGenerator : CodeGenerator {

        private volatile int index;
        private volatile Object threadLock;
        private int maxThreadCount;

        private DTOContainer dtoContainer;
        private DTOInstance dtoObject;
        private SemaphoreSlim semaphore;
        private String path;

        public DTOGenerator(String path,int maxThreadCount) {
            threadLock = new Object();
            this.maxThreadCount = maxThreadCount;
            index = 0;
            semaphore = new SemaphoreSlim(maxThreadCount, maxThreadCount);
            this.path = @path;
        }

        //Create .cs file
        private void CreateCSFile() {
            lock (threadLock) {
                Executor executor = new Executor();
                dtoContainer = executor.ThreadSafeExecute(dtoObject.Items,ref index);
                var outputFileName = path + dtoContainer.ClassName + ".cs";
                var generatedCode = GenerateClassCode(dtoContainer,"dtoContainer");
                File.WriteAllText(outputFileName,generatedCode);
            }
        }

        

        //Start generating .cs file (calling from main method)
        public void StartGenerating(DTOInstance dtoObj) {
            dtoObject = dtoObj;
            var finished = new CountdownEvent(1); // Used to wait for the completion of all work items.
            for (int i = 0; i < dtoObject.Items.Count; i++) {
                finished.AddCount();
                semaphore.Wait();
                ThreadPool.QueueUserWorkItem(
                    (state) => {
                        try {
                            CreateCSFile();
                        }
                        finally {
                            semaphore.Release();
                            finished.Signal();
                        }
                    }
                    ,null);
            }
            finished.Signal();
            finished.Wait();
            
        }


    }
}
