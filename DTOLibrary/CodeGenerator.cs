using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPP_DTOGenerator.DTOLib {
    public class CodeGenerator {
        //T4 code generation (without parameters)
        public String GenerateClassCode() {
            ClassTemplate templateInstance = new ClassTemplate();
            templateInstance.Initialize();
            return templateInstance.TransformText(); 
        }

        //T4 code generation (with parameters)
        public String GenerateClassCode(Object container,String containerName) {
            ClassTemplate templateInstance = new ClassTemplate();        // create template T4 object
            templateInstance.Session = new Dictionary<String, Object>();
            templateInstance.Session.Add(containerName, container);   // pass dtoContainer to ClassTemplate.tt
            templateInstance.Initialize();
            return templateInstance.TransformText();
        }
    }
}
