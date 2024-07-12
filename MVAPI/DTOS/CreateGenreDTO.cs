namespace MVAPI.DTOS
{
    public class CreateGenreDTO
    {        
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
