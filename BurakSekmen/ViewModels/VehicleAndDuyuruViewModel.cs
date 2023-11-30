using BurakSekmen.Models;
using System.ComponentModel.DataAnnotations;

namespace BurakSekmen.ViewModels
{
    public class VehicleAndDuyuruViewModel
    {
        public List<Vehicle> Vehicles { get; set; }

        public List<Duyuru> Duyuru { get; set;}

        public List<User> Users { get; set; }


    }
}
