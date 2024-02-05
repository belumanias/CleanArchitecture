using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Director : BaseDomainModel
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }

        public int VideoId { get; set; }
        public virtual Video? Video { get; set; }
    }
}
