using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HalakAPI.Models;

public partial class Horgaszok
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public int? Eletkor { get; set; }

    [JsonIgnore]
    public virtual ICollection<Fogasok> Fogasoks { get; set; } = new List<Fogasok>();
}
