using LoginDemo.Models;
using LoginDemo.Models.EFModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
namespace LoginDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _host;
        private readonly TestContext _db;
       

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment host, TestContext db)
        {
            _logger = logger;
            _host = host;
            _db = db;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginVM vm)
        {
            var acount = _db.Users.Where(o => o.Acount == vm.Acount).FirstOrDefault();
            if (acount != null)
            {
                if (acount.Status == false)
                {
                    TempData["ErrMessage"] = "此帳號待審核開通或是密碼錯誤";

                }
                else
                {
                    TempData["ErrMessage"] = "登入成功!!";
                    Syslog syslog = new Syslog();
                    syslog.Acount = vm.Acount;
                    string strHostName = string.Empty;
                    IPHostEntry ipEnrty = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress[] addr = ipEnrty.AddressList;
                    for (int i = 0; addr.Length > 0; i++)
                    {
                        syslog.Ipaddress = addr[i].ToString();
                    }
                    _db.Syslog.Add(syslog);
                    _db.SaveChanges();
                }
            }
            else
            {
                TempData["ErrMessage"] = "尚未註冊!!";
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM vm, IFormFile File)
        {

            if (!ModelState.IsValid) { return View(vm); }
            var filedto = new Users()
            {
                Name = vm.Name,
                Password = vm.Password,
                Email = vm.Email,
                Birthday = vm.Birthday,
                Acount = vm.Acount,
                Status = false
            };

            var t = _db.Orgs.Where(o => o.Title == vm.Title).FirstOrDefault();
            if (t != null)
            {
                //寫進資料庫
                //將Org_id寫入users資料表裡的Org_id
                filedto.OrgId = t.Id;
            }
            else//當資料庫沒有這筆資料時 
            {
                //建立單位  店家先進貨
                //todo 進貨流程 建立單位的流程
                //將vm的title寫進org資料表裡
                var org = new Orgs()
                {
                    Title = vm.Title,
                };
                _db.Orgs.Add(org);
                _db.SaveChanges();
                org.OrgNo = org.Id;
                _db.SaveChanges();
                //將新增的org_id寫入user資料表 客人買飲料
                //todo 買飲料流程  將新增的org_id寫進資料表的流程
                filedto.OrgId = org.Id;
            }
            _db.Users.Add(filedto);
            _db.SaveChanges();


            //將guid隨機的檔名寫進applyfile資料表裡面的filepath
            var fileName = Guid.NewGuid().ToString("N");
            var apply = new ApplyFile()
            {
                FilePath = fileName,
                //將userid寫進applyfile資料表裡面的userid
                UserId = filedto.Id
            };
            //將檔案存檔
            string rootPath = Path.Combine(_host.WebRootPath, "uploads", fileName);

            using (var fileStream = new FileStream(rootPath, FileMode.Create))
            {
                File.CopyTo(fileStream);
            }
            _db.ApplyFile.Add(apply);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult UserFile(string txtKeyword)
        {
            IEnumerable<Users> datas = null;
            TestContext context = new TestContext();
            if (string.IsNullOrEmpty(txtKeyword))
            {
                datas = from t in context.Users
                        select t;
            }
            else
            {
                datas = context.Users.Where(o => o.Name.Contains(txtKeyword)).ToList();

            }

            return View(datas);
        }





        [HttpPost]
        public IActionResult UserFile(int Id)
        {
            Users status = _db.Users.Where(o=>o.Id ==Id).FirstOrDefault();
           
            status.Status = true;
            _db.SaveChanges();

            TempData[]
            var file = _db.Users.Where(o => o.Status == false).ToList();
            return View(file);
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
    }
}