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
    
    public partial class OrganizationIdentityLink
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int IdentityId { get; set; }
        public int LinkTypeId { get; set; }
        public bool Active { get; set; }
    
        public virtual Identity Identity { get; set; }
        public virtual LinkType LinkType { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
