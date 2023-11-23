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

        public string Resim { get; set; }

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
