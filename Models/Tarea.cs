using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// para crear una tabla nueva crear una clase
// en este caso es para la tabla Tarea. Las propiedades son los campos.
// luego agregar cada modelo (esta clase por ej) al DBContext

namespace mhTestApi.Models
{
    public partial class Tarea
    {
        [Key]
        public int IdTarea { get; set; }

        [StringLength(40)]
        public string nombre { get; set; }
    }
}
