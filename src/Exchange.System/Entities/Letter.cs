using Exchange.System.Enums;
using System;
using System.Collections.Generic;

namespace Exchange.System.Entities
{
    public class Letter : IRequestObject
    {
        public int Id { get; set; }
        public LetterType Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ICollection<Source> Sources { get; set; }
        public int? SenderId { get; set; }
        public DateTime DateCreate { get; set; } // дико обязательно юзать

        public int[] sourcesId = new int[0];
    }
}
