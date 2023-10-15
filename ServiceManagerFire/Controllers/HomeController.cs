using Microsoft.AspNetCore.Mvc;
using ServiceManagerFire.Models;
using System.Diagnostics;
using ServiceManagerFire.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceManagerFire.Controllers
{
    public class HomeController : Controller
    {
        static FireContext db = new();
        List<Customer> listcustomers;
        List<Objekt> listobjekts;
        List<User> listusers;
        List<RoleManager> listrolemanagers;
        List<Status> liststatuses;
        List<Incident> listincidents;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // АВТОРИЗАЦИЯ
        [HttpGet]
        public IActionResult Index()
        {
            Account account = new Account();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Account account)
        {
            listusers = db.Users.ToList();
            listrolemanagers = db.RoleManagers.ToList();
            
                       string role = null;
            if (db.Users == null)
            {
                return NotFound();
            }
            foreach (var item in listusers)
            {
                if ((item.Name == account.NameUser) && (item.Password == account.PassUser)) 
                {
                        role = item.RoleManager.Name.ToString();
                        account.RoleUser = role;
                        break;
                
                }
                
                
            }

            if (role == "админ")
            {
                return RedirectToAction("IndexUser");
            }
            if (role == "руководитель")
            {
                return RedirectToAction("IndexIncidentSuper");
            }
            if (role == "исполнитель")
            {
                return RedirectToAction("IndexIncidentWorker");
            }


            return RedirectToAction("Index");
        }


        //     ЗАКАЗЧИКИ

        public async Task<IActionResult> IndexCustomer()
        {
            return View(db.Customers.ToList());
        }
        public async Task<IActionResult> IndexCustomerSuper()
        {
            return View(db.Customers.ToList());
        }
        public IActionResult CreateCustomer()
        {
            return View(); ;
        }
        public IActionResult CreateCustomerSuper()
        {
            return View(); ;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            db.Customers.Add(customer);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexCustomer");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomerSuper(Customer customer)
        {
            db.Customers.Add(customer);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexCustomerSuper");
        }
        [HttpGet]
        public async Task<IActionResult> EditCustomer(int? id) //редактирование
        {
            if (id != null)
            {

                return View(db.Customers.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> EditCustomerSuper(int? id) //редактирование
        {
            if (id != null)
            {

                return View(db.Customers.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {           
            var trackedCustomer = db.Customers.Find(customer.Id);

            trackedCustomer.Name = customer.Name;
            trackedCustomer.City = customer.City;
            trackedCustomer.Description = customer.Description;

            await db.SaveChangesAsync();
            return RedirectToAction("IndexCustomer");
        }
        [HttpPost]
        public async Task<IActionResult> EditCustomerSuper(Customer customer)
        {
            var trackedCustomer = db.Customers.Find(customer.Id);

            trackedCustomer.Name = customer.Name;
            trackedCustomer.City = customer.City;
            trackedCustomer.Description = customer.Description;

            await db.SaveChangesAsync();
            return RedirectToAction("IndexCustomerSuper");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveCustomer(int id) //удаление 
        {
            if (db.Customers == null)
            {
                return NotFound();
            }

            var customer = await db.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCustomer(int? id, bool notUsed)
        {

            if (id != null)
            {
                Customer? customer = await db.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexCustomer");
                }
            }
            return BadRequest();

        }

        [HttpGet]
        public async Task<IActionResult> RemoveCustomerSuper(int id) //удаление 
        {
            if (db.Customers == null)
            {
                return NotFound();
            }

            var customer = await db.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCustomerSuper(int? id, bool notUsed)
        {

            if (id != null)
            {
                Customer? customer = await db.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexCustomerSuper");
                }
            }
            return BadRequest();

        }

        //      ОБЪЕКТЫ

        public async Task<IActionResult> IndexObjekt()
        {
            listcustomers = db.Customers.ToList();
            return View(db.Objekts.ToList());
        }
        public async Task<IActionResult> IndexObjektSuper()
        {
            listcustomers = db.Customers.ToList();
            return View(db.Objekts.ToList());
        }
        public IActionResult CreateObjekt()
        {           
            listcustomers = db.Customers.ToList();
            ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
            
            return View(); ;
        }
        public IActionResult CreateObjektSuper()
        {
            listcustomers = db.Customers.ToList();
            ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");

            return View(); ;
        }
        [HttpPost]
        public async Task<IActionResult> CreateObjekt(Objekt objekt)
        {
            db.Objekts.Add(objekt);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexObjekt");
        }
        [HttpPost]
        public async Task<IActionResult> CreateObjektSuper(Objekt objekt)
        {
            db.Objekts.Add(objekt);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexObjektSuper");
        }
        [HttpGet]
        public async Task<IActionResult> EditObjekt(int? id) //редактирование
        {
            if (id != null)
            {
                //  List<Customer> listcustomers;

                listcustomers = db.Customers.ToList();
                ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
                return View(db.Objekts.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> EditObjektSuper(int? id) //редактирование
        {
            if (id != null)
            {
                //  List<Customer> listcustomers;

                listcustomers = db.Customers.ToList();
                ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
                return View(db.Objekts.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditObjekt(Objekt objekt)
        {            
            listcustomers = db.Customers.ToList();
            ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
            var trackedObjekt = db.Objekts.Find(objekt.Id);

            trackedObjekt.Name = objekt.Name;
            trackedObjekt.Address = objekt.Address;
            trackedObjekt.CustomerId = objekt.CustomerId;

            await db.SaveChangesAsync();
            return RedirectToAction("IndexObjekt");
        }
        [HttpPost]
        public async Task<IActionResult> EditObjektSuper(Objekt objekt)
        {
            listcustomers = db.Customers.ToList();
            ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
            var trackedObjekt = db.Objekts.Find(objekt.Id);

            trackedObjekt.Name = objekt.Name;
            trackedObjekt.Address = objekt.Address;
            trackedObjekt.CustomerId = objekt.CustomerId;

            await db.SaveChangesAsync();
            return RedirectToAction("IndexObjektSuper");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveObjekt(int id) //удаление 
        {
            if (db.Objekts == null)
            {
                return NotFound();
            }

            var objekt = await db.Objekts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objekt == null)
            {
                return NotFound();
            }

            return View(objekt);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveObjekt(int? id, bool notUsed)
        {

            if (id != null)
            {
                Objekt? objekt = await db.Objekts.FirstOrDefaultAsync(p => p.Id == id);
                if (objekt != null)
                {
                    db.Objekts.Remove(objekt);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexObjekt");
                }
            }
            return BadRequest();

        }
        [HttpGet]
        public async Task<IActionResult> RemoveObjektSuper(int id) //удаление 
        {
            if (db.Objekts == null)
            {
                return NotFound();
            }

            var objekt = await db.Objekts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objekt == null)
            {
                return NotFound();
            }

            return View(objekt);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveObjektSuper(int? id, bool notUsed)
        {

            if (id != null)
            {
                Objekt? objekt = await db.Objekts.FirstOrDefaultAsync(p => p.Id == id);
                if (objekt != null)
                {
                    db.Objekts.Remove(objekt);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexObjektSuper");
                }
            }
            return BadRequest();

        }

        //      ПОЛЬЗОВАТЕЛИ  ОТВЕТСТВЕННЫЕ ЛИЦА

        public async Task<IActionResult> IndexUser()
        {
            listrolemanagers = db.RoleManagers.ToList();
            return View(db.Users.ToList());
        }
        public IActionResult CreateUser()
        {
            listrolemanagers = db.RoleManagers.ToList();
            ViewBag.RoleManager = new SelectList(listrolemanagers, "Id", "Name");

            return View(); ;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            db.Users.Add(user);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexUser");
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(int? id) //редактирование
        {
            if (id != null)
            {
                listrolemanagers = db.RoleManagers.ToList();
                ViewBag.RoleManager = new SelectList(listrolemanagers, "Id", "Name");
                return View(db.Users.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            listrolemanagers = db.RoleManagers.ToList();
            ViewBag.RoleManager = new SelectList(listrolemanagers, "Id", "Name");

            var trackedUser = db.Users.Find(user.Id);

            trackedUser.Name = user.Name;
            trackedUser.Password = user.Password;
            trackedUser.RoleManagerId = user.RoleManagerId;

            await db.SaveChangesAsync();
            return RedirectToAction("IndexUser");
        }

       
        [HttpGet]
        public async Task<IActionResult> RemoveUser(int id) //удаление 
        {
            if (db.Users == null)
            {
                return NotFound();
            }

            var user = await db.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(int? id, bool notUsed)
        {
           
            if (id != null)
            {
                User? user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexUser");
                }
            }
            return BadRequest();

        }

        public async Task<IActionResult> DetailsUser(int? id)
        {


            if (id == null || db.Users == null)
            {
                return NotFound();
            }

            var user = await db.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

            //      РОЛИ ПОЛЬЗОВАТЕЛЕЙ

            public async Task<IActionResult> IndexRoleManager()
        {
            return View(db.RoleManagers.ToList());
        }
        public IActionResult CreateRoleManager()
        {
            return View(); ;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleManager(RoleManager roleManager)
        {
            db.RoleManagers.Add(roleManager);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexRoleManager");
        }
        [HttpGet]
        public async Task<IActionResult> EditRoleManager(int? id) //редактирование
        {
            if (id != null)
            {

                return View(db.RoleManagers.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditRoleManager(RoleManager roleManager)
        {
            var trackedRole = db.RoleManagers.Find(roleManager.Id);

            trackedRole.Name = roleManager.Name;


            await db.SaveChangesAsync();
            return RedirectToAction("IndexRoleManager");
        }
        [HttpGet]
        public async Task<IActionResult> RemoveRoleManager(int id) //удаление 
        {
            if (db.RoleManagers == null)
            {
                return NotFound();
            }

            var roleManager = await db.RoleManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleManager == null)
            {
                return NotFound();
            }

            return View(roleManager);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRoleManager(int? id, bool notUsed)
        {

            if (id != null)
            {
                RoleManager? roleManager = await db.RoleManagers.FirstOrDefaultAsync(p => p.Id == id);
                if (roleManager != null)
                {
                    db.RoleManagers.Remove(roleManager);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexRoleManager");
                }
            }
            return BadRequest();

        }

        //      СТАТУС ЗАЯВОК

        public async Task<IActionResult> IndexStatus()
        {
            return View(db.Statuses.ToList());
        }
        public IActionResult CreateStatus()
        {
            return View(); ;
        }
        [HttpPost]
        public async Task<IActionResult> CreateStatus(Status status)
        {
            db.Statuses.Add(status);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexStatus");
        }
        [HttpGet]
        public async Task<IActionResult> EditStatus(int? id) //редактирование
        {
            if (id != null)
            {

                return View(db.Statuses.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditStatus(Status status)
        {
            var trackedStatus = db.Statuses.Find(status.Id);

            trackedStatus.Name = status.Name;


            await db.SaveChangesAsync();
            return RedirectToAction("IndexStatus");
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> RemoveStatus(int id) //удаление 
        {
            if (db.Statuses == null)
            {
                return NotFound();
            }

            var status = await db.Statuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveStatus(int? id, bool notUsed)
        {

            if (id != null)
            {
                Status? status = await db.Statuses.FirstOrDefaultAsync(p => p.Id == id);
                if (status != null)
                {
                    db.Statuses.Remove(status);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexStatus");
                }
            }
            return BadRequest();

        }

        //      ЗАЯВКИ


        public async Task<IActionResult> IndexIncident(string? incidentUser, string? st)
        {
            listcustomers = db.Customers.ToList();
            listobjekts = db.Objekts.ToList();
            listusers = db.Users.ToList();
            listrolemanagers = db.RoleManagers.ToList();
            liststatuses = db.Statuses.ToList();
           
            if (db.Incidents == null)
            {
                return NotFound();
            }

            IQueryable<string> userQuery = from m in db.Incidents
                                            orderby m.User.Name
                                            select m.User.Name;
            IQueryable<string> statusQuery = from m in db.Incidents
                                           orderby m.Status.Name
                                           select m.Status.Name;

            var incidents = from m in db.Incidents
                         select m;

            if (!String.IsNullOrEmpty(st))
            {
                incidents = incidents.Where(s => s.Status.Name!.Contains(st));
            }

            if (!String.IsNullOrEmpty(incidentUser))
            {
                incidents = incidents.Where(x => x.User.Name == incidentUser);
            }

            var incidentUserVM = new StatusUserViewModel
            {
                Users = new SelectList(await userQuery.Distinct().ToListAsync()),
                Statuses = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Incidents = await incidents.ToListAsync()
            };

            return View(incidentUserVM);
//            return View(db.Incidents.ToList());
        }
        public async Task<IActionResult> IndexIncidentSuper(string? incidentUser, string? st)
        {
            listcustomers = db.Customers.ToList();
            listobjekts = db.Objekts.ToList();
            listusers = db.Users.ToList();
            listrolemanagers = db.RoleManagers.ToList();
            liststatuses = db.Statuses.ToList();

            if (db.Incidents == null)
            {
                return NotFound();
            }

            IQueryable<string> userQuery = from m in db.Incidents
                                           orderby m.User.Name
                                           select m.User.Name;
            IQueryable<string> statusQuery = from m in db.Incidents
                                             orderby m.Status.Name
                                             select m.Status.Name;

            var incidents = from m in db.Incidents
                            select m;

            if (!String.IsNullOrEmpty(st))
            {
                incidents = incidents.Where(s => s.Status.Name!.Contains(st));
            }

            if (!String.IsNullOrEmpty(incidentUser))
            {
                incidents = incidents.Where(x => x.User.Name == incidentUser);
            }

            var incidentUserVM = new StatusUserViewModel
            {
                Users = new SelectList(await userQuery.Distinct().ToListAsync()),
                Statuses = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Incidents = await incidents.ToListAsync()
            };

            return View(incidentUserVM);
            //            return View(db.Incidents.ToList());
        }

        public async Task<IActionResult> IndexIncidentWorker(string? incidentUser, string? st)
        {
            listcustomers = db.Customers.ToList();
            listobjekts = db.Objekts.ToList();
            listusers = db.Users.ToList();
            listrolemanagers = db.RoleManagers.ToList();
            liststatuses = db.Statuses.ToList();

            if (db.Incidents == null)
            {
                return NotFound();
            }

            IQueryable<string> userQuery = from m in db.Incidents
                                           orderby m.User.Name
                                           select m.User.Name;
            IQueryable<string> statusQuery = from m in db.Incidents
                                             orderby m.Status.Name
                                             select m.Status.Name;

            var incidents = from m in db.Incidents
                            select m;

            if (!String.IsNullOrEmpty(st))
            {
                incidents = incidents.Where(s => s.Status.Name!.Contains(st));
            }

            if (!String.IsNullOrEmpty(incidentUser))
            {
                incidents = incidents.Where(x => x.User.Name == incidentUser);
            }

            var incidentUserVM = new StatusUserViewModel
            {
                Users = new SelectList(await userQuery.Distinct().ToListAsync()),
                Statuses = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Incidents = await incidents.ToListAsync()
            };

            return View(incidentUserVM);
            //            return View(db.Incidents.ToList());
        }

        public IActionResult CreateIncident()
        {
            listcustomers = db.Customers.ToList();
            listobjekts = db.Objekts.ToList();
            listusers = db.Users.ToList();
            listrolemanagers = db.RoleManagers.ToList();
            liststatuses = db.Statuses.ToList();
            ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
            ViewBag.Objekt = new SelectList(listobjekts, "Id", "Name");
            ViewBag.User = new SelectList(listusers, "Id", "Name");
            ViewBag.RoleManager = new SelectList(listrolemanagers, "Id", "Name");
            ViewBag.Status = new SelectList(liststatuses, "Id", "Name");

            return View(); ;
        }
        public IActionResult CreateIncidentSuper()
        {
            listcustomers = db.Customers.ToList();
            listobjekts = db.Objekts.ToList();
            listusers = db.Users.ToList();
            listrolemanagers = db.RoleManagers.ToList();
            liststatuses = db.Statuses.ToList();
            ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
            ViewBag.Objekt = new SelectList(listobjekts, "Id", "Name");
            ViewBag.User = new SelectList(listusers, "Id", "Name");
            ViewBag.RoleManager = new SelectList(listrolemanagers, "Id", "Name");
            ViewBag.Status = new SelectList(liststatuses, "Id", "Name");

            return View(); ;
        }
        [HttpPost]
        public async Task<IActionResult> CreateIncident(Incident incident)
        {
            db.Incidents.Add(incident);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexIncident");
        }
        [HttpPost]
        public async Task<IActionResult> CreateIncidentSuper(Incident incident)
        {
            db.Incidents.Add(incident);//добавление
            await db.SaveChangesAsync();
            return RedirectToAction("IndexIncidentSuper");
        }
       

        [HttpGet]
        public async Task<IActionResult> EditIncident(int? id) //редактирование
        {
            if (id != null)
            {
              

                listcustomers = db.Customers.ToList();
                listobjekts = db.Objekts.ToList();
                listusers = db.Users.ToList();
                listrolemanagers = db.RoleManagers.ToList();
                liststatuses = db.Statuses.ToList();
                ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
                ViewBag.Objekt = new SelectList(listobjekts, "Id", "Name");
                ViewBag.User = new SelectList(listusers, "Id", "Name");
                ViewBag.RoleManager = new SelectList(listrolemanagers, "Id", "Name");
                ViewBag.Status = new SelectList(liststatuses, "Id", "Name");
                return View(db.Incidents.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> EditIncidentSuper(int? id) //редактирование
        {
            if (id != null)
            {


                listcustomers = db.Customers.ToList();
                listobjekts = db.Objekts.ToList();
                listusers = db.Users.ToList();
                listrolemanagers = db.RoleManagers.ToList();
                liststatuses = db.Statuses.ToList();
                ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
                ViewBag.Objekt = new SelectList(listobjekts, "Id", "Name");
                ViewBag.User = new SelectList(listusers, "Id", "Name");
                ViewBag.RoleManager = new SelectList(listrolemanagers, "Id", "Name");
                ViewBag.Status = new SelectList(liststatuses, "Id", "Name");
                return View(db.Incidents.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> EditIncidentWorker(int? id) //редактирование
        {
            if (id != null)
            {


                listcustomers = db.Customers.ToList();
                listobjekts = db.Objekts.ToList();
                listusers = db.Users.ToList();
                listrolemanagers = db.RoleManagers.ToList();
                liststatuses = db.Statuses.ToList();
                ViewBag.Customer = new SelectList(listcustomers, "Id", "Name");
                ViewBag.Objekt = new SelectList(listobjekts, "Id", "Name");
                ViewBag.User = new SelectList(listusers, "Id", "Name");
                ViewBag.RoleManager = new SelectList(listrolemanagers, "Id", "Name");
                ViewBag.Status = new SelectList(liststatuses, "Id", "Name");
                return View(db.Incidents.FirstOrDefault(p => p.Id == id));

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditIncident(Incident incident)
        {

            

            var trackedIncident = db.Incidents.Find(incident.Id);

            trackedIncident.ObjektId = incident.ObjektId;
            trackedIncident.Description = incident.Description;
            trackedIncident.UserId = incident.UserId;
            trackedIncident.StatusId = incident.StatusId;
            trackedIncident.DateTime = incident.DateTime;
           

            await db.SaveChangesAsync();
            return RedirectToAction("IndexIncident");
        }
        [HttpPost]
        public async Task<IActionResult> EditIncidentSuper(Incident incident)
        {
          
            var trackedIncident = db.Incidents.Find(incident.Id);

            trackedIncident.ObjektId = incident.ObjektId;
            trackedIncident.Description = incident.Description;
            trackedIncident.UserId = incident.UserId;
            trackedIncident.StatusId = incident.StatusId;
            trackedIncident.DateTime = incident.DateTime;


            await db.SaveChangesAsync();
            return RedirectToAction("IndexIncidentSuper");
        }
        [HttpPost]
        public async Task<IActionResult> EditIncidentWorker(Incident incident)
        {          

            var trackedIncident = db.Incidents.Find(incident.Id);

            
            trackedIncident.StatusId = incident.StatusId;
           


            await db.SaveChangesAsync();
            return RedirectToAction("IndexIncidentWorker");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveIncident(int id) //удаление 
        {
            if (db.Incidents == null)
            {
                return NotFound();
            }

            var incident = await db.Incidents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveIncident(int? id, bool notUsed)
        {

            if (id != null)
            {
                Incident? incident = await db.Incidents.FirstOrDefaultAsync(p => p.Id == id);
                if (incident != null)
                {
                    db.Incidents.Remove(incident);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexIncident");
                }
            }
            return BadRequest();

        }
        [HttpGet]
        public async Task<IActionResult> RemoveIncidentSuper(int id) //удаление 
        {
            if (db.Incidents == null)
            {
                return NotFound();
            }

            var incident = await db.Incidents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveIncidentSuper(int? id, bool notUsed)
        {

            if (id != null)
            {
                Incident? incident = await db.Incidents.FirstOrDefaultAsync(p => p.Id == id);
                if (incident != null)
                {
                    db.Incidents.Remove(incident);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexIncidentSuper");
                }
            }
            return BadRequest();

        }

        public async Task<IActionResult> DetailsIncident(int? id)
        {

            
                if (id == null || db.Incidents == null)
                {
                    return NotFound();
                }

                var incident = await db.Incidents
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (incident == null)
                {
                    return NotFound();
                }

                return View(incident);
            

        }
        public async Task<IActionResult> DetailsIncidentSuper(int? id)
        {


            if (id == null || db.Incidents == null)
            {
                return NotFound();
            }

            var incident = await db.Incidents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);


        }

        public async Task<IActionResult> DetailsIncidentWorker(int? id)
        {


            if (id == null || db.Incidents == null)
            {
                return NotFound();
            }

            var incident = await db.Incidents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);


        }






        // не нужно
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