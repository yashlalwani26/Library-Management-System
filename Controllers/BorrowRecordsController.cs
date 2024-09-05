namespace Web_Application.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web_Application.Models;

public class BorrowRecordsController : Controller
{
    private readonly LibraryContext _context;

    public BorrowRecordsController(LibraryContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var borrowRecords = _context.BorrowRecords
            .Include(br => br.Book)
            .Include(br => br.Member)
            .ToList();
        return View(borrowRecords);
    }

    public IActionResult Create()
    {
        ViewData["Books"] = new SelectList(_context.Books.Where(b => b.IsAvailable), "BookId", "Title");
        ViewData["Members"] = new SelectList(_context.Members, "MemberId", "Name");
        return View();
    }

    [HttpPost]
    public IActionResult Create(BorrowRecord borrowRecord)
    {
        if (ModelState.IsValid)
        {
            var book = _context.Books.Find(borrowRecord.BookId);
            if (book != null && book.IsAvailable)
            {
                book.IsAvailable = false;
                _context.BorrowRecords.Add(borrowRecord);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        ViewData["Books"] = new SelectList(_context.Books.Where(b => b.IsAvailable), "BookId", "Title");
        ViewData["Members"] = new SelectList(_context.Members, "MemberId", "Name");
        return View(borrowRecord);
    }

    public IActionResult ReturnBook(int id)
    {
        var borrowRecord = _context.BorrowRecords.Include(br => br.Book).FirstOrDefault(br => br.BorrowRecordId == id);
        if (borrowRecord == null)
        {
            return NotFound();
        }

        borrowRecord.ReturnDate = DateTime.Now;
        borrowRecord.Book.IsAvailable = true;

        _context.BorrowRecords.Update(borrowRecord);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}

