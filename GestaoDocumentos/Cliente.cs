using System.ComponentModel.DataAnnotations;

namespace GestaoDocumentos
{
    public class Cliente
    {
        #region Properties
        public int FurnisherId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Street { get; set; }
        [Required] public string PostalCode { get; set; }
        [Required] public string Siret { get; set; }

        public DateTime Date { get; set; }

        #endregion
    }
    public class FurnisherRegister
    {
        #region Properties
        [Required] public string Name { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Street { get; set; }
        [Required] public string PostalCode { get; set; }
        [Required] public string Siret { get; set; }
        #endregion
    }
}
