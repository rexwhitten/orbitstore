//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrbitStore.Web.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Binder
    {
        public Binder()
        {
            this.BinderResourceLink = new HashSet<BinderResourceLink>();
            this.IdentityBinderLink = new HashSet<IdentityBinderLink>();
        }
    
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<BinderResourceLink> BinderResourceLink { get; set; }
        public virtual ICollection<IdentityBinderLink> IdentityBinderLink { get; set; }
    }
}