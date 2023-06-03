using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_CORE.BLL.Interfaces;
using MVC_CORE.DDL.Entities;
using MVC_CORE_Application.Consts;
using MVC_CORE_Application.Models.ViewModel;

namespace MVC_CORE_Application.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchValue)
        {
            if(string.IsNullOrEmpty(searchValue)){ 
             var categories = await UnitOfWork.CategoryRepository.GetAll();
            var categoriesViewModel = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);
            return View(categoriesViewModel);
            }
            var searchedCategory = await UnitOfWork.CategoryRepository.searchByName(searchValue);
            var searchesCategoriesViewModel = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(searchedCategory);
            return View(searchesCategoriesViewModel);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Form");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<CategoryViewModel, Category>(model);
                category.CreatedOn = DateTime.Now;
                await UnitOfWork.CategoryRepository.Add(category);
                return RedirectToAction(nameof(Index));
            
            
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            if (id == null)
                return NotFound();
            var category = await UnitOfWork.CategoryRepository.GetById(id);
            if(category is null)
                return NotFound();
            var categoryViewModel = Mapper.Map<Category, CategoryViewModel>(category);
            return View("Form" , categoryViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<CategoryViewModel, Category>(model);
                category.LastUpdatedOn = DateTime.Now;
                await UnitOfWork.CategoryRepository.Update(category);
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id) {
            if (id == null)
                return NotFound();
            var category = await UnitOfWork.CategoryRepository.GetById(id);
            if (category is null)
                return NotFound();
            var categoryViewModel = Mapper.Map<Category, CategoryViewModel>(category);
            return View("Details", categoryViewModel);
        }
        [Authorize(Roles = Role.Admin)]

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();
            var category = await UnitOfWork.CategoryRepository.GetById(id);
            if (category is null)
                return NotFound();
            var categoryViewModel = Mapper.Map<Category, CategoryViewModel>(category);
            return View("Delete", categoryViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
         
                var category = Mapper.Map<CategoryViewModel, Category>(model);
               
                await UnitOfWork.CategoryRepository.Delete(category);
                return RedirectToAction(nameof(Index));

         
        }
    }
}
