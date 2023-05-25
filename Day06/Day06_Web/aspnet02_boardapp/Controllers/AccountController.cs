using aspnet02_boardapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.Contracts;

namespace aspnet02_boardapp.Controllers
{
    // 사용자 회원가입, 로그인, 로그아웃
    public class AccountController : Controller
    {
        

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            // 생성자 마법사. Null 검사 추가
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        // https://localhost:7059/Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // public IActionResult Register(RegisterModel model)
        // 비동기가 아니면 return 값은 IActionResult(위)
        // 비동기면 Task<IActionResult>(아래)
        public async Task<IActionResult> Register(RegisterModel model)
        {
            ModelState.Remove("PhoneNumber"); // PhoneNumber는 입력값 검증에서 제거
            if (ModelState.IsValid) // 데이터 입력 후 검증 성공 시
            {
                var user = new IdentityUser()
                {
                    // ASP.NET user - aspnetusers 테이블에 데이터넣기 위해서
                    // 매핑되는 인스턴스를 생성
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                // aspnetusers 테이블에 사용자 데이터를 대입
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // 회원가입을 성공했으면 로그인한 뒤 localhost:7059/Home/Index로!
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    // 회원가입 후 토스트메시지 띄우기
                    TempData["succeed"] = "회원가입 성공했습니다."; // 성공메세지
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model); // 회원가입을 실패하면 그 화면 그대로 유지
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // 파라미터 순서 : 아이디, 패스워드, isPersistent = RemeberMe, 실패할 때 계정 잠그기
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // TODO : 로그인 후 토스트메시지 띄우기
                    TempData["succeed"] = "로그인 했습니다."; // 성공메세지
                    return RedirectToAction("Index", "Home");

                }

                ModelState.AddModelError("", "로그인 실패!");
            }

            return View(model); // 입력검증이 실패하면 화면에 그대로 대기
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // TODO : 로그아웃 후 토스트메시지 띄우기
            TempData["succeed"] = "로그아웃 했습니다."; // 성공메세지
            return RedirectToAction("Index", "Home");
        }
    }
}