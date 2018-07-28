(**
- title : F#nctional F#n
- description : F#nctional F#n
- author : Andy Hutchinson
- theme : night
- transition : default

***

<img src="images/fsharp512.png" style="background: transparent; border-style: none;"  width="512" />

# F#

***

# F#

<br />
<br />

## Welcome to the Functional Programming Appreciation Society

<br />
<br />

Andy Hutchinson 

***

## F#n Quiz

...

---

<img src="images/ferrari.jpg" style="background: transparent; border-style: none;"  width="800" />

### Name that car...

---

<img src="images/f-16.jpg" style="background: transparent; border-style: none;"  width="800" />

### Name the plane...

---

<img src="images/florence.jpg" style="background: transparent; border-style: none;"  width="800" />

### Name that place...

---

<img src="images/flashgordon.jpg" style="background: transparent; border-style: none;"  width="800" />

### Name the superhero...

---

<img src="images/foofighters.jpg" style="background: transparent; border-style: none;"  width="800" />

### Name the band...

---

### Name a cool programming language starting with "F"...

---

<img src="images/fortran.png" style="background: transparent; border-style: none;"  width="800" />

---

<img src="images/fsharp2.jpeg" style="background: transparent; border-style: none;"  width="800" />

***

## Agenda 

<section>
  <ul>
    <li class="fragment" style="color:#ffffff">Intro</li>
    <li class="fragment" style="color:#ffffff">Boring Example</li>
    <li class="fragment" style="color:#ffffff">F#od for Thought</li>
    <li class="fragment" style="color:#ffffff">Brief and Indisputably Accurate History of Programming Languages</li>
    <li class="fragment" style="color:#ffffff">Character Assassination of Object Oriented Language Design Features</li>
  </ul>
  <br>
  <br>
  <h4 class="fragment">About 30 minutes of pain...</h4>
</section>

---

## What is F#

##### Microsoft's Functional Programming Language
<br>
##### V1.0 released in 2005 - Available within Visual Studio since 2010
<br>
##### Full compatibility with .NET Libs / Nuget / C# - 50:50 Solutions
<br>
##### Developed @ Microsoft Research, UK
##### Based on ML / StandardML / OCaml / Haskell
<br>
##### Source of inspiration for C#: 
##### (Pattern matching, Tuples, Deconstruction, Local Functions, Nullable Reference Types, Records, Ranges...)

--- 

## Why am I telling you about F#?
<br><br>
<ul>
 <li> I spent a year as an F# developer
 <li> Restored my faith in <span style="text-decoration: line-through;">humanity</span> computers
 <li> More concise and convenient - Get more done
 <li> Amazing Compiler - Type Inference - Far Fewer Bugs
 <li> Open Source
 <li> Community - e.g. JavaScript Transpiler (this presentation...) 
</ul>
 
***

## Dull Example

<section>
  <h3 class="fragment"> Absolute Classic... </h3>
  <h3 class="fragment"> ...Shapes</h3>
  <h4 class="fragment">Areas / Perimeters</h4>
  <ul>
    <li class="fragment" style="color:#ff0000">Circle</li>
    <li class="fragment" style="color:#00ffff">Rectangle</li>
    <li class="fragment" style="color:#8888ff">Square</li>
    <li class="fragment" style="color:#00ff00">Triangle</li>
  </ul>
  <br />
  <br />
  <h3 class="fragment"> C# vs F# </h3>
</section> 

---

- data-background : #888800

### C# - Shape

    [lang=cs]
    interface Shape
    {
        double Area();
        double Perimeter();
    }

---

- data-background : #880000

### C# - Circle

    [lang=cs]
    class Circle : Shape
    {
        private double radius;

        public Circle(double r) {
            this.radius = r;
        }

        public double Area() {
            return System.Math.PI * radius * radius;
        }

        public double Perimeter() {
            return 2 * System.Math.PI * radius;
        }
    }

---

- data-background : #008888

### C# - Rectangle

    [lang=cs]
    class Rectangle : Shape
    {
        private double width;
        private double height;

        public Rectangle(double w, double h) {
            width = w;
            height = h;
        }

        public double Area() {
            return width * height;
        }

        public double Perimeter() {
            return 2 * (width + height);
        }
    }

---

- data-background : #444488

### C# - Square

    [lang=cs]
    // Wanting to use inheritance stops us using value types / structs
    class Square : Rectangle 
    {
        public Square(double side) : base(side, side) {}
    }

---

- data-background : #008800

### C# - Triangle

    [lang=cs]
    class Triangle : Shape
    {
        private double baseLength;
        private double perpendicularHeight;

        public Triangle(double b, double h) {
            baseLength = b;
            perpendicularHeight = h;
        }

        public double Area() {
            return 0.5 * baseLength * perpendicularHeight;
        }

        public double Perimeter() {
            //return Math.Sqrt(baseLength * baseLength + perpendicularHeight * perpendicularHeight) + baseLength + perpendicularHeight;
            throw new NotImplementedException();
        }
    }

*** 
- data-background : images/stripes.gif

### F# Shapes

*)

type Shape =                             // Discriminated Union
  | Circle of radius:double                            // Cases
  | Square of side:double 
  | Rectangle of width:double * height:double
  | Triangle of baseLength:double * perpHeight:double

let perimeter s =                            // (No {brackets}) 
  match s with                                  // No arg types
  | Circle r -> 2.0 * System.Math.PI * r           // No return
  | Rectangle (w, h) -> 2.0 * (w + h)
  | Square l -> 4.0 * l
  | Triangle (b, h) -> System.Math.Sqrt (b * b + h * h) + b + h  

let rec area s = 
  match s with         // Stripey vs Blocky (2 sides same coin)
  | Circle r -> System.Math.PI * r * r  // Type safe, all cases
  | Rectangle (w, h) -> w * h 
  | Square l -> area (Rectangle (l, l))                    // !
  | Triangle (b, h) -> 0.5 * b * h     

(**

---

## In Use..
<br>
### C# - Square

    [lang=cs]
    var a = (new Square(5.0)).Area();

    var e = (new Circle(3.0) == new Circle(3.0));       // False
    var f = (new Circle(3.0).Equals(new Circle(3.0)));  // False
<br>
### F# - Square

*)

let a = area <| Square 5.0

let ce = (Circle 3.0 = Circle 3.0)
(*** include-value: ce ***)
(**

***

### Cake

![Paul Mary Cake](images/paulmarycake.jpg)

#### Recipe ??

---

### Mary Berry Cake Recipe

![Mary Cake](images/maryberrycake.jpg)

---

### Mary's Wonderful Sponge Cake

- Break the eggs into a large mixing bowl.
- Add the sugar, flour, baking powder and butter. 
- Mix together until well combined with an electric hand mixer.
- Divide the mixture evenly between the tins.
- Bake the cakes on the middle shelf of the oven for 25 minutes.

---

### Evil Paul Hollywood Cake Recipe

![Paul Cake](images/paulcake.jpg)

---

### Paul has been Reading a Book...

![Design Patterns](images/designpatterns.jpeg)

#### ...to help him achieve his plan for world domination

---

### Paul's Mass Produced Cake

    [lang=cs]
    mixingBowl.Add(eggs.Break());
    mixingBowl.Add(new Ingredients [] {sugar, flour, bakingPowder, butter});

    mixingBowl.AcceptVisit(foodMixer);

    cakeTin1.AddMixture(mixingBowl.Take(0.5));
    cakeTin2.AddMixture(mixingBowl.Take(0.5));

    var oven = ovenFactory.CreateOven(OvenType.GAS);
    oven.SetTemperature(180.0);
    oven.Load(cakeTin1);
    oven.Load(cakeTin2);
    oven.SetTimer(25 * 60 * 1000);

    try {
        oven.Bake();
    } catch (TimeUpException e) {
        //
    }

---

### OO // Land of the Nouns
<br><br>
#### Data Centric -> Classes Are King
##### Methods Belong To Classes
##### -> Verbs Belong To Nouns
<br><br>
##### != English
##### != Natural Thought Process
##### OO-Induced Damage...
##### ... the code reading can be difficult 
##### ... the real world modeling of is slightly strange 

***

### Brief and _Indisputably Accurate_ History of Programming Languages

##### All types of programming invented at roughly the same time

![Kernighan and Richie](images/kandr.jpg)

##### But... Procedural Languages Became Popular First

---

##### "Best" Thing In Procedural Languages were Structs / Data Records

    [lang=c]
    struct account {
       int account_number;
       char *first_name;
       char *last_name;
       float balance;
    };

---

##### OO Languages Became Popular Next

![bjarne-stroustrups](images/bjarne-stroustrups-quotes-1.jpg)

---

#### OO Took the "Best" Thing In Procedural Languages & Made it "Better"

    [lang=c++]
    #include <iostream>
    using namespace std;

    class Account {
        int account_number;
        char *first_name;
        char *last_name;
        float balance;
      public:
        void Charge (float v);
        void Refund (float v);
        void Close (char *reason);
    };

##### Structs -> Classes with Functions
##### Nouns own Verbs
##### Massive Design "Decision"

---

### Functional Programming Has Hit The Big Time
 
![functional logos](images/functional.jpeg)

##### #ProgrammingLikeFunctionsMatter #KingdomOfTheVerbs

##### PUT FUNCTIONS FIRST + Different Design Decisions
##### OO generalises over Data but FP generalises over Processes

---

### 1st Class Functions

- Killer Feature... All functions can take only 1 argument

*)

let multiply a b = a * b              // two functions (curried)
let multiplyBy2 = multiply 2              // partial application
let multiplyBy4 = multiplyBy2 >> multiplyBy2 // composition/glue

let r = multiplyBy4 3

(*** include-value: r ***) 

let result = 
  [1..10]                       
  |> List.map multiplyBy4        // pipeline - function as value
  |> List.filter ((<>) 24)        // operator / function duality

(*** include-value: result ***)  

(**

---

### No Variables (Immutability)
##### Generally accepted that global variables are bad...

    [lang=cs]
    public double factorial(int n) { // 1 * 2 * 3 * ...  * n
        double result = 1;
        for(int i = 2; i<=n; ++i) {
            result *= i;
        }
        return result;
    }

*)

let rec factorial = function // recursive - no variables, no loops
    | 0 | 1 -> 1
    | n -> n * factorial (n-1)
(**
  Or... **1 liner** (Same as definition)
*)
let factorialB n = [1..n] |> List.reduce (*)   // Map / reduce
let fb = factorialB 9                    // do factorial 9 !!!
(*** include-value: fb ***)
(**

---

### Immutability - Entire Programs - Messages & Agents

- MailboxProcessors / Actors / Agents (like Erlang / Akka)
- Handling: Events / Messages

*)

let newState oldState event = () // (State minimization)

(**

- Async / Multithreaded (helped by Immutability)
- [Mario](http://fable.io/samples-browser/mario/)
- [Pacman](http://fable.io/samples-browser/pacman/)

***

### Assassination Attempt

![Bond](images/bond.jpg)

---

### 50 Billion Dollar Mistake

![Tony Hoare](images/tony.jpg)

- Tony Hoare - Algol 60 (London, 1960s)
- Admitted his mistake in 2009
- ('Fixed' in C# 8.0 ... vs F# 1.0)

---

### Null References

*)

let t = Triangle (3.0, 4.0)

let triangleArea = area t
//let triangleArea2 = area null  // !! This is a compile error !!

let maybeCalculateArea s =                      // Option types
    match s with
    | Some shape -> area shape
    | None -> 0.0

let someTriangle = Some <| Triangle (3.0, 4.0)
let noTriange = None

let areaSomeTriangle = maybeCalculateArea someTriangle
let areaNoTriangle = maybeCalculateArea noTriange

(*** include-value: areaSomeTriangle ***) 
(*** include-value: areaNoTriangle ***) 
(**

---

### Exceptions 

![Fred Brooks - Mythical Man Month](images/fredbrooks.jpg)

- Fred Brooks - PL/1 - 1960s
- Mythical Man Month
- To be fair, if you have **null** then you kind of need **exceptions** too...

---

### Railway Oriented Programming

*)

// 2 Tracks / Pipelines

type Result<'TSuccess,'TFailure> = 
| Success of 'TSuccess 
| Failure of 'TFailure

let (>=>) f g a = 
    match f a with
    | Success s -> g s
    | Failure f -> Failure f  

let processWebRequest =
  parseUrlPath
  >=> parseUrlParameters
  >=> parseRequestBodyFormValues
  >=> processAction
  >=> buildResponse
  >=> writeResponse
  >=> handleErrors

(**

***


### ...Recap... ((Esc))

#### There's More...

##### OO Features!!!, Mutability!!, Tuples, Records, Lists, Scripts, Interactive, Workflows (Async, Query, Seq...)

![Scott](images/Scott.jpg)
##### [F# for Fun and Profit](https://fsharpforfunandprofit.com/)

*)