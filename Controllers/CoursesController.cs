using CourseNest.Data;
using CourseNest.Models.Entities;
using CourseNest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseNest.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CoursesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseViewModel addcourseViewModel)
        {
            var course = new Course
            {
                Name = addcourseViewModel.Name,
                DurationMonths= addcourseViewModel.DurationMonths,
                Fees= addcourseViewModel.Fees,
                Description= addcourseViewModel.Description

            };



            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var courses = await dbContext.Courses.ToListAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var stud = await dbContext.Courses.FindAsync(id);
            return View(stud);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Course st)
        {
            var stud = await dbContext.Courses.FindAsync(st.Id);
            if (stud is not null)
            {
                stud.Name = st.Name;
                stud.DurationMonths = st.DurationMonths;
                stud.Fees= st.Fees;
                stud.Description = st.Description; 

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Courses");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Course st)
        {
            var stud = await dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == st.Id);
            if (stud is not null)
            {
                dbContext.Courses.Remove(stud);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Courses");
        }
    }
}
