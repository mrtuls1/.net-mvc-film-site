namespace MvcVize.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Film = new HashSet<Film>();
        }

        public int kullaniciID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(50)]
        public string soyad { get; set; }

        [StringLength(50)]
        public string eposta { get; set; }

        [StringLength(50)]
        public string sifre { get; set; }

        public int? ilceID { get; set; }

        [StringLength(50)]
        public string resim { get; set; }

        public bool? cinsiyet { get; set; }

        [StringLength(20)]
        public string telefon { get; set; }

        [StringLength(500)]
        public string adres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Film> Film { get; set; }

        public virtual ilceler ilceler { get; set; }
    }
}
