using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BurakSekmen.Models
{
    public class Duyuru
    {
        public int Id { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(600)]
        public string DuyurAcıklama { get; set; }

        public bool Durum { get; set; }
    }
}
