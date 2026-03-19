using API_Lobanov.Context;
using API_Lobanov.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API_Lobanov.Controllers
{
    [Route("api/UsersController")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <returns>Данный метод предназначен для авторизации пользователя на сайте</returns>
        /// <response code="200">Пользователь успешно авторизован</response>
        /// <response code="403">Ошибка запроса, данные не указаны</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("SingIn")] // указываем какой метод вызывается
        [HttpPost] // указываем какой тип запроса используется
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult SingIn([FromForm] string Login, [FromForm] string Password)
        {
            if (Login == null || Password == null)
                return StatusCode(403);

            try
            {
                Users User = new UsersContext().Users.Where(x => x.Login == Login && x.Password == Password).First();
                return Json(User);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <returns>Данный метод предназначен для регистрации пользователя на сайте</returns>
        /// <response code="200">Пользователь успешно зарегестрирован</response>
        /// <response code="403">Ошибка запроса, данные не указаны</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("RegIn")] // указываем какой метод вызывается
        [HttpPost] // указываем какой тип запроса используется
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult RegIn([FromForm] string Login, [FromForm] string Password)
        {
            if (Login == null || Password == null)
                return StatusCode(403);

            try
            {
                using (var context = new UsersContext())
                {
                    var existingUser = context.Users.FirstOrDefault(x => x.Login == Login);

                    if (existingUser != null)
                        return StatusCode(400);

                    var newUser = new Users
                    {
                        Login = Login,
                        Password = Password
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();

                    return Json(newUser);
                }
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }
    }
}