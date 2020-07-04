using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bloq
{
    [Serializable]

    public class Save
    {
        [DataMember]
        public List<Figure> fig;
        [DataMember]
        public int heigh;
        [DataMember]
        public int width;

    }
}
