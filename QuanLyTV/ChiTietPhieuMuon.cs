//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyTV
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietPhieuMuon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChiTietPhieuMuon()
        {
            this.PhieuPhats = new HashSet<PhieuPhat>();
        }
    
        public long MaPhieuMuon { get; set; }
        public string Masach { get; set; }
        public Nullable<System.DateTime> NgayTra { get; set; }
        public Nullable<System.DateTime> NgayMuon { get; set; }
        public string TrangThai { get; set; }
    
        public virtual PhieuMuon PhieuMuon { get; set; }
        public virtual PhieuMuon PhieuMuon1 { get; set; }
        public virtual Sach Sach { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuPhat> PhieuPhats { get; set; }
    }
}
