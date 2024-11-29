using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UniquenessCheckELMA.Application;

public static class DbContextExtension
{
    public static bool TrySaveChangesAsync(this DbContext _context, ControllerBase controller, out IActionResult result)
    {
        try
        {
            var res = _context.SaveChangesAsync().Result;
            result = default!;
            return true;
        }
        catch (DbUpdateConcurrencyException) // DbUpdateConcurrencyException - исключение, возникающее при попытке обновления записи в базе данных, которая была изменена или удалена другим пользователем
        {
            result = controller.Conflict("Не удалось обновить запись в базе данных из-за конфликта параллелизма (параллельного доступа)");
        }
        catch (DbUpdateException) // DbUpdateException - исключение, которое генерируется при ошибке в базе данных
        {
            result = controller.Problem("Не удалось обновить запись в базе данных из-за ошибки в базе данных");
        }
        catch (OperationCanceledException) // OperationCanceledException - исключение, которое генерируется при отмене операции
        {
            result = controller.StatusCode(StatusCodes.Status503ServiceUnavailable, "Операция была отменена");
        }
        catch (Exception ex)
        {
            var message = new StringBuilder();
            var exception = ex;
            while (exception != null)
            {
                message.AppendLine(exception.Message);
                exception = exception.InnerException;
            }
            result = controller.Problem(message.ToString());
        }
        return false;
    }

}
