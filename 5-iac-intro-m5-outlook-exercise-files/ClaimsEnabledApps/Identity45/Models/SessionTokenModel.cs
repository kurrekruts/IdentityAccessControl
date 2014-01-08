using System;
using System.ComponentModel;

namespace Identity45.Models
{
    public class SessionTokenModel
    {
        [DisplayName("Cookie Size (in bytes)")]
        public int Size { get; set; }
        public string Context { get; set; }

        [DisplayName("Context ID")]
        public string ContextId { get; set; }
        
        [DisplayName("Endpoint ID")]
        public string EndpointId { get; set; }
        
        [DisplayName("Is Persistent")]
        public bool IsPersistent { get; set; }
        
        [DisplayName("Is Reference Mode")]
        public bool IsReference { get; set; }
        
        [DisplayName("Valid from")]
        public DateTime ValidFrom { get; set; }
        
        [DisplayName("Valid to")]
        public DateTime ValidTo { get; set; }
        
        [DisplayName("Cookie Expiration")]
        public DateTime CookieExpires { get; set; }
    }
}