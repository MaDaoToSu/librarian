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
    
    public partial class PhieuMuon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuMuon()
        {
            this.ChiTietPhieuMuon = new HashSet<ChiTietPhieuMuon>();
        }
    
        public long MaPhieuMuon { get; set; }
        public Nullable<long> MaDG { get; set; }
        public Nullable<long> MaNV { get; set; }
        public Nullable<System.DateTime> NgayMuon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuon { get; set; }
        public virtual DocGia DocGia { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
