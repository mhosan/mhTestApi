using System;
using System.ComponentModel.DataAnnotations;


namespace mhTestApi.Models
{
    public class ClientMachine
    {
        [Key]
        public int IdClientMachine { get; set; }

        public string MacAddress { get; set; }

        public DateTime Created { get; set; }
    }
}
