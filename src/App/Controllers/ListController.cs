using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class ListController : Controller
{
    private static Qualification _selected = new() { Name = "Rust", Id = 3 };

    private ListViewModel _viewModel = new ListViewModel()
    {
        Qualifications = new List<Qualification>()
        {
            new() { Name = "C#", Id = 1 },
            new() { Name = "Python", Id = 2 },
            _selected
        },
        Selected = _selected.Id
    };

    public IActionResult Index() => View(_viewModel);
}