using ASP_NET_07._TaskFlow_Introduction.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_07._TaskFlow_Introduction.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskItemsController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemsController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }
}
