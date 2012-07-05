using System.Collections.Specialized;
using System.Linq;

namespace CerealBox
{
    public class DynamicNameValueCollection : DynamicDictionary
    {
        public DynamicNameValueCollection(NameValueCollection nameValueCollection) : base(nameValueCollection.AllKeys.ToDictionary(x => x, x => nameValueCollection[x])) { }
    }
}