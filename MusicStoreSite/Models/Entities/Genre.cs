using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStoreSite.Models.Entities
{
    public class Genre
    {
        [Key]
        public int GenreId { set; get; }
        [Required()]
        public string Name { set; get; }


    }
}