@{
    Layout = "post";
    Title = "Asp.net WebApi Cheat Sheet";
    Date = "2020-08-30T14:17:51";
    Tags = "C# asp.net webapi cheat sheet";
    Description = "Aide-memoire for asp.net web-api";
}

** Asp.net: _WebAPI_ **
-------------------------------------------------

**Aide-memiore for asp.net web-api**.

<div class="palette fewerColumnsPalette">

> ## REST Principles ## 
> * Separate Client and Server 
> * Stateless Server
> * Supports Caching (correctly uses caching headers)
> * Layering (services talk only to next layer down)
> * (Optional - Code on Demand (provide JS to client))
> * Uniform Interface (see below)

---

> ## REST Uniform Interface ##
> * Resource Identification - URIs and Response Headers
> * Resource Manipulation - HTTP verbs
> * Self-describing Responses - Media Types
> * State Management - HATEOAS

---

> ## HATEOAS ##
> * Hypermedia as the Engine of Application State
> * In practice, a set of 'links' describing available actions for a resource
> * e.g. 'deposit': '/account/123/deposit, 'withdraw': 'account/123/withdraw'
> * RESTful interaction is driven by hypermedia, rather than out-of-band information.
> * Supports discoverability and very-thin clients.

---

> ## Controllers ##
> * : ControllerBase (Or name ends with Controller, or [Controller])
> * Request -> Routing -> Filters -> Formatters -> Controller Action
> * Response <- Routing <- Filters <- Formatters <- Controller Action
> * [ApiController] 
> * [Route("api/[controller]")] - controller classname minus "Controller" suffix

---

> ## Method Attributes ##
> * [HttpGet] - async Task<ActionResult<IEnumerable<Xyz>>>
> * [HttpGet("{id}")] - async Task<ActionResult<Xyz>>
> * Prefix method name with _http-verb_, or use annotation below
> * [HttpPost] [HttpPut] [HttpDelete] [HttpHead] [HttpOptions] [HttpPatch]
> * [NonAction]
> * return CreatedAtAction(nameof(GetMethod), new { id=x }, item)
> * [HttpPut("{id}")] - async Task<IActionResult>
> * return NoContent()
> * return BadRequest()
> * return NotFound()

---

> ## Model Binding ##
> * Params: [FromBody] [FromForm] [FromHeader] [FromRoute] [FromServices
> * [FromHeader(Name="My-Access-Token")]string accessToken
> * IModelBinder - Task BindModelAsync(ModelBindingContext bc)

---

> ## Model Validation ##
> * [Required, MaxLength(20)]
> * [MinLength(5)]
> * [EmailAddress]
> * [Url]
> * [Range(1, 5)]
> * if (ModelState.IsValid) { .. } else { return BadRequest(ModelState) }
> * : ValidationAttribute - Custom Attribute
> * : IValidatableObject - Implement own Validate method on Dto 

---

> ## Responses ##
> * Methods return IActionResult are most general (allows success/fail?) 
> * JsonResponse, ContentResponse
> * [Produces(...)]
> * Ok(...) / OkObjectResult(...)
> * NotFound() / NotFountResult(...)
> * BadRequest()
> * Json(...)
> * File(...)
> * Created()
> * services.AddMvc(options => options.OutputFormatters.Add(..)

---

> ## Filters ##
> * Authorization Filters - I(Async)AuthorizationFilter
> * Resource Filters - I(Async)ResourceFilter
> * (Model Binding)
> * Action Execution Filters - I(Async)ActionFilter
> * Exception Filters - I(Async)ExceptionFilter
> * Result Filters  - I(Async)ResultFilter
> * On...Executing - Before
> * On...Executed - After
> * On...ExecutionAsync - Both Async, await next() in middle
> * : Attribute - for attribute-controlled filters
> * services.AddMvc(options => options.Filters.Add(..) global

---

> ## Routing ##
> * rtBldr.MapRoute(string.Empty, context => { .. } 
> * rtBldr.MapPost("foo/{id:int}", (req, resp, routeDat) => { .. }
> * rtBldr.MapGet("bar/{number?}", (req, resp, routeDat) => { .. }
> * [Route("api/[controller]")] - base route attribute on Controller
> * [HttpGet("")]
> * [HttpGet("top/{n}")] - relative to controller base path
> * [HttpPost("~/bob/person")] - _not_ relative to controller base path

---

> ## Startup - Configure ##
> * app.UseDeveloperExceptionPage
> * app.UseHttpsRedirection
> * app.UseRouter(...)
> * app.UseAuthorization
> * app.UseEndpoints(ep => ep.MapControllers())
> * Startup[EnvironmentName] class is used by preference
> * app.Use / app.Map / app.MapWhen - very low-level routing

---

> ## Startup - ConfigureServices ##
> * services.AddMvc().AddApplicationPart(extentionAssembly)
> * services.AddDbContext(opt => opt.UseInMemoryDatabase("DbName")
> * services.AddControllers()
> * services.AddRouting()
> * service.AddSingleton<IHostedService, MyBackgroundService>()

---

> ## Configuration ##
> * new ConfigurationBuilder().SetBasePath(..).AddJsonFile(..)
> * .AddCommandLine(args) .AddEnvironmentalVariables() .AddUserSecrets(>>)
> * Configuration = builder.Build()
> * var value = Configuration["foo:bar"]

---

> ## Program ##
> * Minimal main: WebHost.CreateDefaultBuilder(args).Build().Run()
> * WebHostBuilder.UseKestral() / WebHostBuilder.UseHttpSys()
> * WebHostBuilder.UseIISIntegration()
> * WebHostBuilder.UseStartup<Startup>()
> * WebHostBuilder.UseContentRoot(...)
> * WebHostBuilder.ConfugureAppConfiguration(...)
> * WebHostBuilder.UseUrls (WebHostBuilder.PreferHostingUrls)
> * WebHostBuilder.ConfigureLogging(logs => logs.AddConsole()) 
> * WebHostBuilder.ConfigureLogging(logs => logs.SetMinimumLevel(..)) 
> * WebHostBuilder.Build()
> * WebHostBuilder.GetSetting()

---

> ## Logging ##
> * Inject ILogger into constructors
> * logger.LogTrace(..)
> * logger.LogDebug(..)
> * logger.LogInformation(..)
> * logger.LogWarning(..)
> * logger.LogError(..)
> * logger.LogCritical(..)
> * using (logger.BeginScope("Some scope")) { .. }

---

> ## Standard Folders ##
> * Controllers
> * Dtos
> * Models

---

> ## Useful Commands ##
> * dotnet new webapi -o MyProjectFolder
> * dotnet add package Microsoft.EntityFrameworkCore.SqlServer
> * dotnet add package Microsoft.EntityFrameworkCore.InMemory
> * dotnet add pageage Microsoft.EntityFrameworkCore.Design
> * dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
> * dotnet tool install --global dotnet-aspnet-codegenerator
> * dotnet tool update -g dotnet-aspnet-codegenerator
> * dotnet aspnet-codegenerator controller -name XyzController -async -api -m Xyx -dc AbcContext -outDir Controllers


---

</div>

### _FParsec **Pipes**_ ###

[FParsec Pipes](https://github.com/rspeele/FParsec-Pipes) is an independent extension to FParsec, written by Robert Peele.
It adds some new operators and combinators that make it (arguably) more intuitive to translate a grammar into a parser.
The [documentation](http://rspeele.github.io/FParsec-Pipes/Intro.html) for Pipes is actually pretty concise, but I wanted
a single page reference to both libraries so I've documented some of it below.

<div class="palette fewerColumnsPalette">

> ## Default Parsers ##
> * %"a specific string" // pstring ...
> * %ci "case insensitive" // pstringCI ...
> * %'c' // pchar ...
> * %ci 'c' // pcharCI ...
> * %['a'; 'b'] // choice [...]
> * %["hello"; "there"] // choice [...]
> * %[pint32; pMyParser] // choice [...]

---

> ## Parsers For Types ##
> * %p< int > // pint32
> * %p< uint16 > // puint16
> * %p< float > // pfloat
> * %p< Position > // getPosition

---

> ## Pipes ##
> * %% // start a pipe
> * -- // build pipe
> * +. // capture element
> * -|> // empty captured pipe elements to func
> * -%> auto // empty captured pipe elements to tuple
> * ?- // wrap left side in an attempt
> * -? // attempts left side and then .>>.? right

---

> ## Repeats and Separations ##
> * pA * qty.[3..] // 3+ pAs
> * qty.[2..4] / ',' * pA // 2 to 4 pAs - sepBy comma
> * qty.[..5] / ',' * pA // up to 5 pAs - sepEndBy comma

---

</div>