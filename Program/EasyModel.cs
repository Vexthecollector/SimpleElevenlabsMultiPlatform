using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleElevenlabsMultiPlatform
{
    public class EasyModel
    {
        public EasyModel(String id, String name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
