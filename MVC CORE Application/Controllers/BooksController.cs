using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_CORE.BLL.Interfaces;
using MVC_CORE.DDL.Entities;
using MVC_CORE_Application.Consts;
using MVC_CORE_Application.Helpers;
using MVC_CORE_Application.Models.ViewModel;

namespace MVC_CORE_Application.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchValue)
        {
            if(string.IsNullOrEmpty(searchValue)){ 
             var books = await UnitOfWork.BookRepository.GetAll();
            var booksViewModel = Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
            return View(booksViewModel);
            }
            var searchedBook = await UnitOfWork.BookRepository.searchByName(searchValue);
            var searchedBooksViewModel = Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(searchedBook);
            return View(searchedBooksViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await UnitOfWork.CategoryRepository.GetAll();
            return View("Form");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            { model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                var book = Mapper.Map<BookViewModel ,Book>(model);
                book.CreatedOn = DateTime.Now;
                await UnitOfWork.BookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await UnitOfWork.CategoryRepository.GetAll();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
                return NotFound();
            var book = await UnitOfWork.BookRepository.GetById(id);
            if (book is null)
                return NotFound();
            var bookViewModel = Mapper.Map<Book, BookViewModel>(book);
            ViewBag.Categories = await UnitOfWork.CategoryRepository.GetAll();

            return View("Form", bookViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image,"images");
                var book = Mapper.Map<BookViewModel, Book>(model);
                book.LastUpdatedOn = DateTime.Now;
                await UnitOfWork.BookRepository.Update(book);
                return RedirectToAction(nameof(Index));

            }
            ViewBag.Categories = await UnitOfWork.CategoryRepository.GetAll();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return NotFound();
            var book = await UnitOfWork.BookRepository.GetById(id);
            if (book is null)
                return NotFound();
            var bookViewModel = Mapper.Map<Book, BookViewModel>(book);
            ViewBag.Categories = await UnitOfWork.CategoryRepository.GetAll();

            return View("Details", bookViewModel);
        }
        [Authorize(Roles =Role.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();
            var book = await UnitOfWork.BookRepository.GetById(id);
            if (book is null)
                return NotFound();
            var bookViewModel = Mapper.Map<Book, BookViewModel>(book);
            ViewBag.Categories = await UnitOfWork.CategoryRepository.GetAll();

            return View("Delete", bookViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BookViewModel model)
        {

            var book = Mapper.Map<BookViewModel, Book>(model);

            if (model.ImageName != null) { 
            DocumentSettings.DeleteFile(model.ImageName, "images");

            }       
            await UnitOfWork.BookRepository.Delete(book);
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(CategoryViewModel model)
        //{

        //        var category = Mapper.Map<CategoryViewModel, Category>(model);

        //        await UnitOfWork.CategoryRepository.Delete(category);
        //        return RedirectToAction(nameof(Index));


        //}
    }
}
