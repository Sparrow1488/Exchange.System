using ExchangeSystem.Requests.Objects;
using System;

namespace ExchangeSystemCore.Requests.Objects.Entities
{
    public class Source : IRequestObject
    {
        public int Id { get; set; }
        public string Base64Data { get; set; }
        public int? SenderId { get; set; }
        public string Extension { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
