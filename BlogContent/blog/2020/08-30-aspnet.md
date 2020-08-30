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

> ## Controller Attributes ##
> * : ControllerBase
> * [ApiController] 
> * [Route("api/[controller]")] - controller classname minus "Controller" suffix

---

> ## Startup - Configure ##
> * app.UseDeveloperExceptionPage
> * app.UseHttpsRedirection
> * app.UseRouting
> * app.UseAuthorization
> * app.UseEndpoints(ep => ep.MapControllers())

---

> ## Startup - ConfigureServices ##
> * services.AddDbContext(opt => opt.UseInMemoryDatabase("DbName")
> * services.AddControllers()

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