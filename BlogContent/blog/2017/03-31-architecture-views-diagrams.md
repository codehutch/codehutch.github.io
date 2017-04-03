@{
    Layout = "post";
    Title = "Architectural Views and Diagrams";
    Date = "2017-03-31T08:44:17";
    Tags = "Architecture Software Views Diagrams";
    Description = "Overview of view and diagrams for documenting software architectures";
}

** Software Architecture: Views _and_ Diagrams **
-------------------------------------------------

**A comparison of various standard sets of software architecture viewspoints / diagrams**. People who like
technicalities will want it made clear that strictly speaking there is a difference between a _viewpoint_
and the choice of _diagram_ chosen to depict it. With that _(not really)_ highly important point duly noted,
here's a look at some of the main diagramatic options knocking around the world of software systems design...

### _4 **PLUS** 1_ ###

Let's start with the simple option. Philippe Kruchten (of Rational Software) proposes using a relatively
small set of 5 viewspoints _(4+1)_ to descibe a software system. Five may seem _now_ like quite a lot of
diagrams, but it's going to get a whole lot worse further down the page, so just be grateful for small
mercies. The idea is that you use _one_ of each of the first four types (**logical, process, development & physical**)
and then use _a few_ **scenario** views to illustrate key use cases. Scenario views can use a combination of
the drawing elements from the logical and process views.

| _Viewpoint:_        | Logical View                              | Process View                        | Development View              | Physical View                     | Scenario Views                                               |
|---------------------|-------------------------------------------|-------------------------------------|-------------------------------|-----------------------------------|--------------------------------------------------------------|
| **Depicts:**        | Functionality / Objects Users Manipultate | Processing / Rhythms / Interactions | Implementation / Organisation | Deployment / Operation / Topology | Use Cases                                                    |
| **Concerns:**       | End-user focus                            | Dynamics / Communication / NFRs     | Re-use / Sharing / Layering   | Operability / Support             | Objects and Interactions                                     |
| **Components:**     | Classes                                   | Tasks / Threads                     | Modules / Packages            | VMs / DBs / Servers               | Steps / Scripts                                              |
| **Connectors:**     | Association / Inheritance / Composition   | Messages / RPC / IPC / HTTP / WS    | Dependencies / Usages         | Networking Protocols / Mediums    | Paths                                                        |
| **Containers:**     | Class Categories                          | Processes / Programs / Services     | Libraries / Subsystems        | Data-centres / Zones / Networks   | _Generally combines elements from Logical and Process Views_ |
| **UML Equivalent:** | Class Diagram                             | Activity Diagram                    | Component / Package Diagram   | Deployment Diagram                | Use Case Diagram                                             |

Here's the original 1995 paper about [4+1](http://www.cs.ubc.ca/~gregor/teaching/papers/4+1view-architecture.pdf)

#### _UML_ ####

Weighing in with a heavy set of diagrams for the up-front design of large software systems, OMG's **Unified Modeling Language**
blows our minds with 14 different ways of scrawling pictures about software. Actually, rather than scrawling, they would
encourage you to use a (_probably expensive_) tool to make everything neat and tidy. Confusion abounds with new diagram
types being added between UML v1 and v2.0 (in 2005), and existing ones renamed. Surely starting to prioritise standardisation
over actual comprehensibility, here's the full list anyway:

##### Static / Structural / Being / Is #####

| _Diagram:_          | Class Diagram                                                                | Object Diagram                                                | Component Diagram | Package Diagram | Composite Structure Diagram+ | Deployment Diagram | Profile Diagram+ |
|---------------------|------------------------------------------------------------------------------|---------------------------------------------------------------|-------------------|-----------------|------------------------------|--------------------|------------------|
| **Depicts:**        | Object Oriented Structure                                                    | Usage of Instances of Classes                                 |                   |                 |                              |                    |                  |
| **Concerns:**       | Domain Models, Module Structures                                             | How to Cater for a particular Task / Function / Scenario      |                   |                 |                              |                    |                  |
| **Components:**     | Classes, Interfaces, Enums                                                   | Instances of Classes / Enums                                  |                   |                 |                              |                    |                  |
| **Connectors:**     | Association, Aggregation, Composition, Generalization, Implementation, Usage | References (Links / Pointers), maybe with Counts and Ordering |                   |                 |                              |                    |                  |
| **Containers:**     | Components can contain Attributes and Methods                                | Instances can be shown containing particular Attribute Values |                   |                 |                              |                    |                  |

##### Dynamic / Behavioural / Doing / Does #####

| _Diagram:_          | Activity Diagram | State Machine Diagram | Use Case Diagram |
|---------------------|------------------|-----------------------|------------------|
| **Depicts:**        |                  |                       |                  |
| **Concerns:**       |                  |                       |                  |
| **Components:**     |                  |                       |                  |
| **Connectors:**     |                  |                       |                  |
| **Containers:**     |                  |                       |                  |

###### Interactions ######

These are all derived from the more general **Behaviour** diagram

| _Diagram:_          | Communication Diagram+ | Interaction Overview Diagram+ | Sequence Diagram | Timing Diagram |
|---------------------|------------------------|-------------------------------|------------------|----------------|
| **Depicts:**        |                        |                               |                  |                |
| **Concerns:**       |                        |                               |                  |                |
| **Components:**     |                        |                               |                  |                |
| **Connectors:**     |                        |                               |                  |                |
| **Containers:**     |                        |                               |                  |                |

[UML](http://www.uml.org) is _looked after_ by [**OMG**](http://www.omg.org). **OMG** stands for _Object Management Group_
(a.k.a. **OMFG**). **Looked after** means _Allowed to grow hideously out of control_.