using System.Collections.Generic;

namespace izibongo.api.DAL.Models
{
    public abstract class LinkedResourceBaseModel
    {
        public List<LinkModel> Links { get; set; } = new List<LinkModel>();
    }

    public class LinkedCollectionResourceModel<T> : LinkedResourceBaseModel
        where T : LinkedResourceBaseModel
    {
        public LinkedCollectionResourceModel(IEnumerable<T> value){
            Value = value;
        }
        public IEnumerable<T> Value { get; set; }
    }

    public class LinkModel
    {
        public LinkModel(
            string href,
            string rel,
            string method
        )
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Method { get; private set; }
    }
}