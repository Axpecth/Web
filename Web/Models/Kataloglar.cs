namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kataloglar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kataloglar()
        {
            this.Urunlers = new HashSet<Urunler>();
        }
    
        public int KatalogID { get; set; }
        public string KatalogAdi { get; set; }
        public string KatalogYol { get; set; }
        public string KatalogResim { get; set; }
        public Nullable<bool> Aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urunler> Urunlers { get; set; }
    }
}
