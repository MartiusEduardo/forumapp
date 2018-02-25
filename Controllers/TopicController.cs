using System.Collections.Generic;
using System.Linq;
using ForumApp.Models;
using ForumApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ForumApp.Controllers{
    public class TopicController : Controller{
        private ApplicationDbContext dbContext;
        private Topic GetTopic(int id) => dbContext.Topic.SingleOrDefault(t => t.idTopic == id);
        private Category GetCategory(int idCategory) => dbContext.Category.SingleOrDefault(c => c.idCategory == idCategory);
        private List<Posting> GetPostsByTopic(int idTopic) => dbContext.Posting.Where(p => p.idTopic == idTopic).ToList();
        private List<Topic> GetTopicsByCategory(int id) => dbContext.Topic.Where(t => t.idCategory == id).ToList();
        private Posting GetPosting(int idPosting) => dbContext.Posting.SingleOrDefault(p => p.idPosting == idPosting);
        public TopicController(){
            dbContext = ApplicationDbContextFactory.Create();
        }
        public ActionResult ListTopicsPerCategory(int idCategory){
            ViewData["idCategory"] = idCategory;
            return View("Topics", GetTopicsByCategory(idCategory));
        }
        public ActionResult TopicDetails(int idTopic){
            TopicViewModel tvm = new TopicViewModel();
            tvm.topic = GetTopic(idTopic);
            tvm.posting = GetPostsByTopic(idTopic);
            if(tvm != null){
                return View("TopicDetails", tvm);
            } else {
                return View("Error");
            }
        }
        [Authorize(Roles = "Admin, User")]
        public ActionResult CreatingTopic(int idCategory){
            ViewData["categoryName"] = GetCategory(idCategory).Name;
            ViewData["idCategory"] = idCategory;
            return View("CreateTopic");
        }
        public ActionResult CreateTopic(Topic topic){
            if(ModelState.IsValid){
                dbContext.Topic.Add(topic);
                dbContext.SaveChanges();
                return View("Topics", GetTopicsByCategory(topic.idCategory));
            } else {
                return View("Error");
            }
        }
        public ActionResult GetToEditTopic(int idTopic){
            return View("EditTopic", GetTopic(idTopic));
        }
        [Authorize(Roles = "Admin, User")]
        public ActionResult EditTopic(Topic topic){
            var target = GetTopic(topic.idTopic);
            if(target != null && ModelState.IsValid){
                dbContext.Entry(target).CurrentValues.SetValues(topic);
                dbContext.SaveChanges();
                return TopicDetails(topic.idTopic);
            } else {
                return View("Error");
            }
        }
        //POSTS PART
        [Authorize(Roles = "Admin, User")]
        public ActionResult CreatePost(Posting posting){
            if (posting.Message == null || posting.Message == "") {
                ViewData["Message"] = "Message cannot be empty";
                ViewData["idTopic"] = posting.idTopic;
                return View("ErrorPost");
            } else if (ModelState.IsValid) {
                dbContext.Posting.Add(posting);
                dbContext.SaveChanges();
                return TopicDetails(posting.idTopic);
            } else {
                return View("Error");
            }
        }
        public ActionResult GetToEditPosting(int idPosting) {
            return View("EditPosting", GetPosting(idPosting));
        }
        public ActionResult EditPosting(Posting posting) {
            var target = GetPosting(posting.idPosting);
            if (target != null && ModelState.IsValid) {
                dbContext.Entry(target).CurrentValues.SetValues(posting);
                dbContext.SaveChanges();
                return TopicDetails(posting.idTopic);
            } else {
                return View("Error");
            }
        }
        public ActionResult DeletePosting(int idPosting) {
            var target = GetPosting(idPosting);
            if (target != null) {
                dbContext.Posting.Remove(target);
                dbContext.SaveChanges();
                return TopicDetails(target.idTopic);
            } else {
                return View("Error");
            }
        }
    }
}