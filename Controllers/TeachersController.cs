using CourseNest.Data;
using CourseNest.Models.Entities;
using CourseNest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseNest.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public TeachersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTeacherViewModel addTeacherViewModel)
        {
            var teacher = new Teacher
            {
                Name = addTeacherViewModel.Name,
                Email = addTeacherViewModel.Email,
                Phone = addTeacherViewModel.Phone,
                Subject=addTeacherViewModel.Subject

            };



            await dbContext.Teachers.AddAsync(teacher);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var teachers = await dbContext.Teachers.ToListAsync();
            return View(teachers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var teacher = await dbContext.Teachers.FindAsync(id);
            return View(teacher);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Teacher t)
        {
            var ts = await dbContext.Teachers.FindAsync(t.Id);
            if (ts is not null)
            {
                ts.Name = t.Name;
                ts.Email = t.Email;
                ts.Phone = t.Phone;
                ts.Subject=t.Subject;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Teachers");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Teacher t)
        {
            var stud = await dbContext.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == t.Id);
            if (stud is not null)
            {
                dbContext.Teachers.Remove(stud);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Teachers");
        }
    }
}
