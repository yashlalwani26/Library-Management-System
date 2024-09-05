namespace Web_Application.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web_Application.Models;

public class MembersController : Controller
{
    private readonly LibraryContext _context;

    public MembersController(LibraryContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var members = _context.Members.ToList();
        return View(members);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Member member)
    {
        if (ModelState.IsValid)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(member);
    }

    public IActionResult Edit(int id)
    {
        var member = _context.Members.Find(id);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    [HttpPost]
    public IActionResult Edit(Member member)
    {
        if (ModelState.IsValid)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(member);
    }

    public IActionResult Delete(int id)
    {
        var member = _context.Members.Find(id);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var member = _context.Members.Find(id);
        _context.Members.Remove(member);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}


