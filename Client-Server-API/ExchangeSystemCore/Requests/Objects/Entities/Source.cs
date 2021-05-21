using ExchangeSystem.Requests.Objects;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExchangeSystem.Requests.Objects.Entities
{
    public class Source : IRequestObject
    {
        [Key]
        public int Id { get; set; }
        public string Base64Data { get; set; }
        public int? SenderId { get; set; }
        public string Extension { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
