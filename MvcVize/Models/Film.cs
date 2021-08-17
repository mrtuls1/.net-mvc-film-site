namespace MvcVize.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Film")]
    public partial class Film
    {
        public int filmID { get; set; }

        public int? kullaniciID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        public string ozet { get; set; }

        [StringLength(50)]
        public string resim { get; set; }

        public int? kategoriID { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}
