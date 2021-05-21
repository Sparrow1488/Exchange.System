using ExchangeSystem.Requests.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExchangeSystemCore.Requests.Objects.Entities
{
    public class Letter : IRequestObject
    {
        [Key]
        public int Id { get; set; }
        public LetterType Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ICollection<Source> Sources { get; set; }
        public int? SenderId { get; set; }
        public DateTime DateCreate { get; set; } // дико обязательно юзать
    }
}
