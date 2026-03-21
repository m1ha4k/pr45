using API_Lobanov.Context;
using API_Lobanov.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API_Lobanov.Controllers
{
    [Route("api/TasksController")] // указываем раздел
    [ApiExplorerSettings(GroupName = "v1")]
    public class TasksController : Controller
    {
        /// <summary>
        /// Получение списка задач
        /// </summary>
        /// <remarks>Данный метод получает список задач, находящийся в базе данных</remarks>
        /// <response code="200">Список успешно получен</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("List")] // указываем какой метод вызывается
        [HttpGet] // указываем какой тип запроса используется
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                // получаем список задач из базы данных
                IEnumerable<Tasks> Tasks = new TaskContext().Tasks;
                return Json(Tasks); // возвращаем ответ в виде JSON
            }
            catch (Exception exp)
            {
                return StatusCode(500); // если возникли неполадки, выдаём 500 ошибку (ошибку сервера)
            }
        }

        /// <summary>
        /// Получение задачи
        /// </summary>
        /// <remarks>Данный метод получает задачу, находящуюся в базе данных</remarks>
        ///  <response code="200">Задача успешно получена</response>
        ///  <response code="500">При выполнении запроса возникли ошибки</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Tasks), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetTaskById(int id)
        {
            try
            {
                Tasks task = new TaskContext().Tasks
                    .FirstOrDefault(x => x.Id == id);

                if (task == null)
                    return NotFound("Задача не найдена");

                return Json(task);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        public IActionResult SearchTasks(string search)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search))
                {
                    return BadRequest("Поисковый запрос не может быть пустым");
                }

                List<Tasks> tasks = new TaskContext().Tasks
                    .Where(x => x.Name.Contains(search))
                    .ToList();

                if (tasks.Count == 0)
                    return NotFound("Задачи не найдены");

                return Json(tasks);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
        /// <summary>
        /// Метод добавления задачи
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод добавляет задачу в базу данных</remarks>
        [Route("Add")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Add([FromForm] Tasks task)
        {
            try
            {
                TaskContext tasksContext = new TaskContext();
                tasksContext.Tasks.Add(task);
                tasksContext.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }
        
    }
}