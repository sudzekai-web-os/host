# LinCtrl host

The host uses Sudzekai.LinCtrl.Hosting and the custom console logger. Run it from the solution with `dotnet run --project Presentation`.

Copy `Presentation/appsettings.example.json` to `Presentation/appsettings.json` for local settings. The host listens on localhost at `LinCtrl:Port` (5199 by default). Database and JWT settings are handled by Sudzekai.LinCtrl.Hosting.

Set `OpenApi:IsEnabled` to `true` to enable the OpenAPI document at `/openapi/v1.json` and the Scalar UI at `/scalar/v1`. Both routes are disabled by default.

In Scalar, select Bearer under Authentication and paste the JWT. Scalar adds it to the Authorization header for requests sent from the UI.
