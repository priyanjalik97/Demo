# dotnetcore.webapi.TodoApi
initial commit:
modified TEMPLATE / EXAMPLE FROM https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api
played with Controllers
  added TodoItemDTOesController and TodoItemDtosController, in addition to existing TodoItemsController
    https://localhost:5001/api/todoitems		original without DTO
    https://localhost:5001/api/todoitemDtos		implemented DTO
    https://localhost:5001/api/todoitemDTOes	experiment with DTO
    https://localhost:5001/weatherforecast		original
added swagger via nuget pkg manager / nugget CLI
  slightly modified ConfigureServices method in startup.cs
  slightly modified Configure method in startup.cs
    ACCORDING TO FOLLOWING ARTICLE
      https://www.codeproject.com/Articles/5277774/How-to-Create-API-in-NET-Core
        https://localhost:5001/swagger/index.html
added UI commit:
added interface for managing to-do items FROM https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-javascript
	DID IMPROVEMENT WITH style="display:none" FOR editForm
