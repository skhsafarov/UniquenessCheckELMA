using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UniquenessCheckELMA.Application;

public static class DbContextExtension
{
    public static bool TrySaveChangesAsync(this DbContext _context, ControllerBase controller, out IActionResult result)
    {
        try
        {
            _context.SaveChangesAsync();
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
        return false;
    }

}
