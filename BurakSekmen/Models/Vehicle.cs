using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurakSekmen.Models
{
    public class Vehicle
    {
        public int Id { get; set; }



        [ForeignKey(nameof(aracYakıt))]
        public int AracYakıId { get; set; }
        [ForeignKey(nameof(AracKategori))]
        public int AracKategorId { get; set; }

        public AracYakıt aracYakıt { get; set; }

        public AracKategori AracKategori { get; set; }

      


        [Column(TypeName = "VarChar")]
        [StringLength(60)]
        public string AracAdı { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(100)]
        public string Arackm { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(10)]
     
        public string AracMotorTip { get; set; }


        [Column(TypeName = "VarChar")]
        [StringLength(2)]
        public string AracKoltukSayisi { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(2)]
        public string AracValizSayisi { get; set; }


        [Column(TypeName = "VarChar")]
        [StringLength(500)]
        public string AracAcıklama { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(500)]
        public string Fiyat { get; set; }


        public string Resim { get; set; }

        public bool Klima { get; set; }
        public bool CocukKoltugu { get; set; }
        public bool Gps { get; set; }
        public bool bagaj { get; set; }
        public bool Music { get; set; }
        public bool EmniyetKemeri { get; set; }
        public bool ArabaYatagi { get; set; }
        public bool dolab { get; set; }
        public bool bluetooth { get; set; }
        public bool arababilgisayarı { get; set; }
        public bool SesGirisi { get; set; }
        public bool ilkyardımcantası { get; set; }
        public bool Arackiti { get; set; }
        public bool uzaktankitleme { get; set; }
        public bool klimakontrol { get;set; }

        public bool KategoriDurum { get; set; }


    }

    public class AracYakıt
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(10)]
        public string AracYakıtTuru { get; set; }

        public ICollection<Vehicle> vehicles { get; set; }
    }


    public class AracKategori
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(10)]
        public string AracKategoriAdi { get; set; }

        public ICollection<Vehicle> vehicles { get; set; }
    }
}
