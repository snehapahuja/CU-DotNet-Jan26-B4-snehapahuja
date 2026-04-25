using CourseAPI.Common;

namespace CourseAPI.Exceptions
{
    public class MiddleWareException
    {
        private readonly RequestDelegate _next;

        public MiddleWareException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                var response = new APIResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
