namespace Web_Application.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web_Application.Models;

public class BooksController : Controller
{
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.Books.ToList();
        return View(books);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        if (ModelState.IsValid)
        {
            book.IsAvailable = true;
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(book);
    }

    public IActionResult Edit(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(book);
    }

    public IActionResult Delete(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var book = _context.Books.Find(id);
        _context.Books.Remove(book);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}


