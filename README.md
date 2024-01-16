Todo API med Autentisering
RamVerk: ASP.NET Core Web API
Programmeringsspråk: C#
Beskrivning:
Det här projektet är en ASP.NET Core Web API för hantering av todo-uppgifter, integrerat med JWT (JSON Web Tokens) baserad autentisering. Det erbjuder funktionaliteter som att skapa, läsa, uppdatera och ta bort todo-uppgifter, var och en länkad till specifika användare som autentiseras genom API:t. Autentiseringen hanteras via en separat kontroller, AuthenticationController, som utfärdar JWT-tokens till validerade användare.

Viktiga Funktioner
Användarautentisering: Använder JWT för säker autentisering.
CRUD-operationer: Möjliggör att skapa, läsa, uppdatera och ta bort todo-uppgifter alltså alla  Crud operationer.
Rollbaserad Åtkomst: Säkerställer att användare endast kan åtkomma sina uppgifter.
Loggning: Implementerar loggning för att spåra API-användning och fel.
Healthycheks
Kontrollers:
AuthenticationController: Hanterar användarautentisering, token-generering och validering av referenser.
TodosController: Hanterar todo-uppgifter, inklusive listning, tillägg, uppdatering och borttagning av uppgifter.
Säkerhet:
Använder JWT för att säkra API:et och säkerställa att endast autentiserade användare kan åtkomma sina todos.
Symmetrisk nyckel för token-generering lagras säkert i konfigurationen.
