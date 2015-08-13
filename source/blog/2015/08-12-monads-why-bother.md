@{
    Layout = "post";
    Title = "Monads >>= Why Bother?";
    Date = "2015-08-12T09:08:30";
    Tags = "";
    Description = "";
}

** MONADS _>>= Why Bother?_ **
------------------------------

##### This is totally unfinished #####

If you stumble across it, please ignore, for now...

In the church of functional programming, monads are glorified with a
mystique that both intrigues and alarms the novice. Bedecked with
a fancy operator (>>=) and a dedicated syntax (do-blocks or
computational expressions), they loom over 'ordinary' code with a
certain holier-than-though smugness. So, are they deserving of their
place on a pedestal? Let's try to see through the fervour, recognise
them for what the really are, and put them at our own disposal.

### _A gift from the functional high ground **OR** Psuedo-mathematical over-complication?_ ###

##### Heaven and Hell #####

Monads help bridge the gap between the utopia that we would like to code in
(heaven) and the depressingly complicated reality that we unfortunately have
to deal with (hell). Here are some examples of this duality:

| Situation      | Heaven                                                         | Hell                                                     |
|----------------|----------------------------------------------------------------|----------------------------------------------------------| 
| Validation     | Users can only enter valid input. There is no need to validate | User input may be invalid in thousands of different ways |
| Error Handling | Nothing can possibly go wrong or be mis-configured             | Database connections may drop. Files always exist        |

How Monads bridge the gap between heaven and hell is by using types.

The Monadic Type is the Hell Type. In other, more positive tutorials,
the Monadic Type will be given a nicer name, like Amplified Type or
or Wrapper Type or Elevated Type, ...
But that is dogma. The Monadic Type is dirty and unclean, Monads primary
benefit is to prevent you from having to get your hands dirty in the
grubby, depressing Hell of the Monadic Type.

#### _Which type of **HELL** would you like?_ ####

| Situation  | Heaven Type (a.k.a. Plain Type)  | Hell Type (a.k.a. Monadic Type / Elevated Type / ... do more) |
|------------|----------------------------------|---------------------------------------------------------------| 
| Validation | Users only enter valid input.    | Users enter a myriad of errors                                |

Non-relationship of >> to >>=

Enlightenment. Transcendental

This is only one view of Monads. Original source (mathematical, haskall, didn't understand all the maths or all the haskall)
Other links.

