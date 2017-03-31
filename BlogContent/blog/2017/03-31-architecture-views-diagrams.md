@{
    Layout = "post";
    Title = "Architectural Views and Diagrams";
    Date = "2017-03-31T08:44:17";
    Tags = "Architecture Software Views Diagrams";
    Description = "Overview of view and diagrams for documenting software architectures";
}

** Software Architecture: Views _and_ Diagrams **
-------------------------------------------------

A comparison of various standard sets of software architecture diagrams, ignoring entirely any
high-minded debate about the difference between a view, a viewpoint and a diagram

### _4 **PLUS** 1_ ###

Philippe Kruchten (of Rational Software) proposes  using a relatively small set of 5
viewspoints to descibe a software system:

| View:               | Logical View                              | Process View                        | Development View              | Physical View                     | Scenario Views           |
|---------------------|-------------------------------------------|-------------------------------------|-------------------------------|-----------------------------------|--------------------------|
| **Depicts:**        | Functionality / Objects Users Manipultate | Processing / Rhythms / Interactions | Implementation / Organisation | Deployment / Operation / Topology | Use Cases                |
| **Concerns:**       | End-user focus                            | Dynamics / Communication / NFRs     | Re-use / Sharing / Layering   | Operability / Support             | Objects and Interactions |
| **Components:**     | Classes                                   | Tasks / Threads                     | Modules / Packages            | VMs / DBs / Servers               | _Combination_            |
| **Connectors:**     | Association / Inheritance / Composition   | Messages / RPC / IPC / HTTP / WS    | Dependencies / Usages         | Networking Protocols / Mediums    | _of_                     |
| **Containers:**     | Class Categories                          | Processes / Programs / Services     | Libraries / Subsystems        | Data-centres / Zones / Networks   | _Logical and_            |
| **UML Equivalent:** | Class Diagram                             | Activity Diagram                    | Component / Package Diagram   | Deployment Diagram                | _Process Views_          |