using System.Linq;
using ForumApp.Models;
using ForumApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ForumApp.Controllers{
    public class CategoryController : Controller{
        private ApplicationDbContext dbContext;
        public CategoryController(){
            dbContext = ApplicationDbContextFactory.Create();
        }
        public ActionResult Index(){
            return View("Index", GetCategoryViewModel());
        }
        private CategoryViewModel GetCategoryViewModel(){
            CategoryViewModel cvm = new CategoryViewModel(){Category = dbContext.Category.ToList(),
            Group = dbContext.Group.ToList()};
            return cvm;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreatingCategory(){
            return View("CreatingCategory", dbContext.Group.ToList());
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category){
            if(ModelState.IsValid){
                dbContext.Category.Add(category);
                dbContext.SaveChanges();
                return View("Index", GetCategoryViewModel());
            } else {
                return View("Error");
            }
        }
    }
}