using System.Collections.Generic;
using System.Xml.Serialization;

namespace WpfApp1
{
    [XmlRoot("ArrayOfModel")]
    public class ArrayOfModel
    {
        [XmlElement("Model")]
        public List<Model> Models { get; set; }
    }

    [XmlRoot("ArrayOfModel1")]
    public class ArrayOfModel1
    {
        [XmlElement("Model1")]
        public List<Model1> Models1 { get; set; }
    }


    public class Model2
    {
        public string Id { get; set; }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
    }

    public class Model1
    {
        public string Id { get; set; }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
    }
    public class Model
    {
        public string Id { get; set; }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }

    }

}
