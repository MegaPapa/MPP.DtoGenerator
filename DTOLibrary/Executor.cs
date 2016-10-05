using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPP_DTOGenerator.DTOLib {

    public class Executor : ISafety{
        private static Object threadLock = new Object();

        //thread-safe execute dtocontainer from list
        public virtual T ThreadSafeExecute<T>(List<T> list,ref int index) {
            lock (threadLock) {
                Object tmpContainer = new Object();
                tmpContainer = list.ElementAt(index);
                index++;
                return (T)tmpContainer;
            }
        }

    }
}
