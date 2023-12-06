using DapperUnitOfWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DapperUnitOfWork.UnitOfWork;

namespace DapperUnitOfWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetAll()
        {
            var temp = await _unitOfWork.CustomerRepository.GetAllAsync();
            return Ok(temp);
        }

        public async Task<IActionResult> FindById()
        {
            var temp = await _unitOfWork.CustomerRepository.FindAsync(1);
            return Ok(temp);
        }

        public async Task<IActionResult> Update()
        {
            var item = await _unitOfWork.CustomerRepository.FindAsync(1);
            item.ModifiedDate = DateTime.Now;
            var temp = await _unitOfWork.CustomerRepository.UpdateAsync(item);
            _unitOfWork.SaveChanges();
            return Ok(temp);
        }

        public async Task<IActionResult> Add()
        {
            var item = new Customer()
            {
                AccountNumber = "test",
                rowguid = Guid.NewGuid(),
            };
            var temp = await _unitOfWork.CustomerRepository.InsertAsync(item);
            _unitOfWork.SaveChanges();
            return Ok(temp);
        }

        public async Task<IActionResult> DeleteById()
        {
            var temp = await _unitOfWork.CustomerRepository.DeleteAsync(30121);
            _unitOfWork.SaveChanges();
            return Ok(temp);
        }
    }
}
