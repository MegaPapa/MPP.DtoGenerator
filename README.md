MPP Lab num.2
===

---
In this lab must realize multithread DTO-Class generator. The structure and the list of classes are set in JSON file. JSON it has the following structure:
```
{
    "classDescriptions": [
        {
            "className": "SomethingWicked",
            "properties": [
                {
                    "name": "FirstProperty",
                    "type": "integer",
                    "format": "int32"
                },
                {
                    "name": "SecondProperty",
                    "type": "integer",
                    "format": "int64"
                },
                {
                    "name": "ThirdProperty",
                    "type": "number",
                    "format": "float"
                }
            ]
        },
        {
            "className": "SomethingGood",
            ...
        }
    ]
}

```
Result DTO-Class is a follows:
```
public sealed class SomethingWicked {
        public int FirstProperty { get; set; }
        public long SecondProperty { get; set; }
        public float ThirdProperty { get; set; }
    }
```