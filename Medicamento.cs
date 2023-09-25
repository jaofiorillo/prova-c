using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace APIExemplo.Models
{
    public class Medicamento
    {
        [JsonProperty("id")]
        [Key]
        public int IdMedicamento { get; set; }

        [JsonProperty("descricao")]
        public string? Descricao { get; set; }

        [JsonProperty("lote")]
        public string? Lote { get; set; }

        [JsonProperty("mesVencimento")]
        public int? MesVencimento { get; set; }

        [JsonProperty("anoVencimento")]
        public int? AnoVencimento { get; set; }

        [JsonProperty("marca")]
        public string? marca { get; set; }

        [JsonProperty("fabricante")]
        public string? fabricante { get; set; }
    }
}
