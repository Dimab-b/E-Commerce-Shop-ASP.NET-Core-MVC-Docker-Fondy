using System.ComponentModel.DataAnnotations;

namespace FirstSiteShopWithMvc.Models;

    public class Blog
    {
     [Key]
     public int Id { get; set; }
    public string? Title { get; set; }
    public string? Anons { get; set; }
    public string? FullText { get; set; }



}

