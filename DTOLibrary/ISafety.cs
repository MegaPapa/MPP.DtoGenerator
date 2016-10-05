using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPP_DTOGenerator.DTOLib {
    interface ISafety {
        T ThreadSafeExecute<T>(List<T> list,ref int index);
    }
}
