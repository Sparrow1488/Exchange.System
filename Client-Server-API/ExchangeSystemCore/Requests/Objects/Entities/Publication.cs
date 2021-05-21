using ExchangeSystem.Requests.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExchangeSystem.Requests.Objects.Entities
{
    public class Publication : IRequestObject
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        //public ICollection<Source> Sources { get; set; }
        public int? SenderId { get; set; }
        public NewsType Type { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
