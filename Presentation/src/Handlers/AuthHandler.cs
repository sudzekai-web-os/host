namespace Presentation.src.Handlers
{
    public class AuthHandler
    {
        private readonly ILogger<AuthHandler> _logger;

        public AuthHandler(ILogger<AuthHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(HttpContext ctx)
        {
        }
    }
}
