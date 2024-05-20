namespace App.Models;

public class ListViewModel
{
    public ICollection<Qualification> Qualifications { get; set; }

    public int? Selected { get; set; }
}